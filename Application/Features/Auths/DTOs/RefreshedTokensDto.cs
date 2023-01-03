using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Features.Auths.DTOs;

public class RefreshedTokensDto
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}