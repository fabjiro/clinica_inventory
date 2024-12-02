using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Auth;

public class AuthLoginUserReqDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}