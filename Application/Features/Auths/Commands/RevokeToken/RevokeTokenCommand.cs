

using Application.Features.Auths.DTOs;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using AutoMapper;
using MediatR;

namespace Application.Features.Auths.Commands.RevokeToken;

public class RevokeTokenCommand : IRequest<RevokedTokenDto>
{
    public string Token { get; set; }
    public string IpAdress { get; set; }
}

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, RevokedTokenDto>
{
    private readonly IAuthService _authService;
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly IMapper _mapper;

    public RevokeTokenCommandHandler(IAuthService authService, AuthBusinessRules authBusinessRules, IMapper mapper)
    {
        _authService = authService;
        _authBusinessRules = authBusinessRules;
        _mapper = mapper;
    }

    public async Task<RevokedTokenDto> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _authService.GetRefreshTokenByToken(request.Token);
        await _authBusinessRules.RefreshTokenShouldBeExist(refreshToken);
        await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken);

        await _authService.RevokeRefreshToken(refreshToken, request.IpAdress, "Revoked without replacement");

        return _mapper.Map<RevokedTokenDto>(refreshToken);
    }
}
