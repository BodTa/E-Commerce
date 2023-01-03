namespace Application.Features.Auths.DTOs;

public class EnabledOtpAuthenticatorDto
{
    public string SecretKey { get; set; }

    public EnabledOtpAuthenticatorDto()
    {
    }

    public EnabledOtpAuthenticatorDto(string secretKey)
    {
        SecretKey = secretKey;
    }
}