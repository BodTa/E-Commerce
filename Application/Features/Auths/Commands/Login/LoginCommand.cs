

using Application.Features.Auths.DTOs;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Application.Services.UserService;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Enums;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Login;

public class LoginCommand : IRequest<LoggedDto>
{
    public UserForLoginDto UserForLoginDto { get; set; }

    public string IpAdress { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedDto>
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly AuthBusinessRules _authBusinessRules;

    public LoginCommandHandler(IAuthService authService, IUserService userService, AuthBusinessRules authBusinessRules)
    {
        _authService = authService;
        _userService = userService;
        _authBusinessRules = authBusinessRules;
    }

    public async Task<LoggedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByEmail(request.UserForLoginDto.Email);
        await _authBusinessRules.UserShouldBeExist(user);
        await _authBusinessRules.UserPasswordShouldBeMatch(user.Id, request.UserForLoginDto.Password);
        LoggedDto loggedDto = new();
        if( user.AuthenticatorType is not AuthenticatorType.None)
        {
            //todo: add authentication with otp and email
        }
        var accessToken = await _authService.CreateAccessToken(user);
        var refreshToken = await _authService.CreateRefreshToken(user, request.IpAdress);
        await _authService.DeleteOldRefreshTokens(user.Id);
        loggedDto.AccessToken = accessToken;
        loggedDto.RefreshToken = refreshToken;
        return loggedDto;
    }
}