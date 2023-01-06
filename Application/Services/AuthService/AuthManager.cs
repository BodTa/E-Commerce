

using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AuthService;

public class AuthManager : IAuthService
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly TokenOptions _tokenOptions;

    public AuthManager(IUserOperationClaimRepository userOperationClaimRepository, IRefreshTokenRepository refreshTokenRepository, 
        ITokenHelper tokenHelper, TokenOptions tokenOptions)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenHelper = tokenHelper;
        _tokenOptions = tokenOptions;
    }
    public async Task<AccessToken> CreateAccessToken(User user)
    {
        IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(u=>u.UserId == user.Id,
            include: u=>u.Include(u=>u.OperationClaim));

        IList<OperationClaim> operationClaims = userOperationClaims.Items.Select(u => new OperationClaim { Id = u.OperationClaimId, Name = u.OperationClaim.Name }).ToList();

        return _tokenHelper.CreateToken(user, operationClaims);
    }

    public Task<RefreshToken> CreateRefreshToken(User user, string ipAdress)
    {
        return Task.FromResult(_tokenHelper.CreateRefreshToken(user, ipAdress));
    }


    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        return await _refreshTokenRepository.AddAsync(refreshToken);
    }

    public async Task DeleteOldRefreshTokens(int userId)
    {
        IList<RefreshToken> refreshTokens = (await _refreshTokenRepository.GetListAsync(r =>
        r.UserId == userId &&
        r.Revoked == null &&
        r.Expires >= DateTime.UtcNow &&
        r.Created.AddDays(_tokenOptions.RefreshTokenTTL) <= DateTime.UtcNow)
            ).Items;
        foreach(RefreshToken refreshToken in refreshTokens) await _refreshTokenRepository.DeleteAsync(refreshToken);
    }

    public async Task<RefreshToken?> GetRefreshTokenByToken(string token)
    {
       return await _refreshTokenRepository.GetAsync(r=>r.Token == token);
    }

    public async Task RevokeRefreshToken(RefreshToken refreshToken,string ipAdress, string? reason = null, string? replacedToken = null)
    {
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAdress;
        refreshToken.ReasonRevoked = reason;
        refreshToken.ReplacedByToken = replacedToken;
        await _refreshTokenRepository.UpdateAsync(refreshToken);
    }

    public async Task RevokeDescendantRefreshTokens(RefreshToken refreshToken,string ipAdress,string reason)
    {
        RefreshToken childToken = await _refreshTokenRepository.GetAsync(r => r.Token == refreshToken.ReplacedByToken);
        if(childToken != null && childToken.Revoked != null && childToken.Expires <= DateTime.UtcNow)
            await RevokeRefreshToken(childToken, ipAdress, reason);
        else await RevokeDescendantRefreshTokens(childToken,ipAdress, reason);
    }

    public async Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAdress)
    {
        RefreshToken newRefreshToken =  _tokenHelper.CreateRefreshToken(user, ipAdress);
        await RevokeRefreshToken(refreshToken, ipAdress, "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }

}
