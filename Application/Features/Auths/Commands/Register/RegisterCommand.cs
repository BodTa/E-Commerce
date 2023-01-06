

using Application.Features.Auths.DTOs;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using MediatR;

namespace Application.Features.Auths.Commands.Register;

public  class RegisterCommand : IRequest<RegisteredDto>
{
    public UserForRegisterDto  UserForRegisterDto{ get; set; }
    public string IpAdress { get; set; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    private readonly AuthBusinessRules _authBusinessRules;

    public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules)
    {
        _userRepository = userRepository;
        _authService = authService;
        _authBusinessRules = authBusinessRules;
    }

    public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password,passwordHash, out passwordSalt);
        User newUser = new()
        {
            Email = request.UserForRegisterDto.Email,
            FirstName = request.UserForRegisterDto.FirstName,
            LastName = request.UserForRegisterDto.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true,
        };
        var createdUser = await _userRepository.AddAsync(newUser);

        var accessToken = await _authService.CreateAccessToken(createdUser);
        var refreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAdress);

        RegisteredDto registeredDto = new() { AccessToken = accessToken, RefreshToken = refreshToken };
        return registeredDto;
    }
}