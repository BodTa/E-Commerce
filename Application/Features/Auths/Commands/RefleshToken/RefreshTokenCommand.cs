

using Application.Features.Auths.DTOs;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.Auths.Commands.RefleshToken;

public class RefreshTokenCommand :  IRequest<RefreshedTokensDto>
{
    public string?  RefleshToken { get; set; }

    public string? ipAdress { get; set; }
}
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshedTokensDto>
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly AuthBusinessRules _authBusinessRules;

    public RefreshTokenCommandHandler(IAuthService authService, IUserService userService, AuthBusinessRules authBusinessRules)
    {
        _authService = authService;
        _userService = userService;
        _authBusinessRules = authBusinessRules;
    }

    public async Task<RefreshedTokensDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await _authService.GetRefreshTokenByToken(request.RefleshToken);
        await _authBusinessRules.RefreshTokenShouldBeExist(refreshToken);

        if (refreshToken.Revoked != null)
            await _authService.RevokeDescendantRefreshTokens(refreshToken, request.ipAdress, $"Attempted to reuse of revoked ancestor token {refreshToken.Token}");
        await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken);

        var user = await _userService.GetById(refreshToken.UserId);

        var newRefreshToken = await _authService.RotateRefreshToken(user, refreshToken, request.ipAdress);
        var addedRefreshToken = await _authService.AddRefreshToken(newRefreshToken);

        await _authService.DeleteOldRefreshTokens(user.Id);

        var createdAccessToken = await _authService.CreateAccessToken(user);
        
        RefreshedTokensDto refreshedTokensDto = new() { AccessToken = createdAccessToken ,RefreshToken = addedRefreshToken};

        return refreshedTokensDto;
    }
}
