using Application.Features.Auths.Commands.Login;
using Application.Features.Auths.Commands.RefleshToken;
using Application.Features.Auths.Commands.Register;
using Application.Features.Auths.Commands.RevokeToken;
using Application.Features.Auths.DTOs;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    private readonly WebAPIConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("WebAPIConfiguration").Get<WebAPIConfiguration>();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {
        LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto, IpAdress = getIpAdress() };
        var result = await Mediator.Send(loginCommand);
        if( result.RefreshToken is not null) setRefreshTokenToCookie(result.RefreshToken);
        return Ok(result.CreateResponseDto());
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromForm] UserForRegisterDto userForRegisterDto)
    {
        RegisterCommand registerCommand = new() { UserForRegisterDto = userForRegisterDto, IpAdress = getIpAdress() };
        var result = await Mediator.Send(registerCommand);
        setRefreshTokenToCookie(result.RefreshToken);
        return Created("", result.AccessToken);
    }

    [HttpGet("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        RefreshTokenCommand refreshTokenCommand = new() { RefleshToken = getRefreshTokenFromCookies(),ipAdress = getIpAdress() };
        var result = await Mediator.Send(refreshTokenCommand);
        setRefreshTokenToCookie(result.RefreshToken);
        return Created("", result.AccessToken);
    }

    [HttpPut("RevokeToken")]
    public async Task<IActionResult> RevokeToken([FromBody(EmptyBodyBehavior =EmptyBodyBehavior.Allow)] string? refreshToken)
    {
        RevokeTokenCommand revokeTokenCommand = new() { Token = refreshToken ?? getRefreshTokenFromCookies(), IpAdress = getIpAdress() };
        var result = Mediator.Send(revokeTokenCommand);
        return Ok(result);
    }
    private string? getRefreshTokenFromCookies()
    {
        return Request.Cookies["refreshToken"];
    }

    private void setRefreshTokenToCookie(RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(7) };
        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }
}


