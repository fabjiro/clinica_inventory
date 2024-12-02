namespace Application.Dto.User;

public class AuthUserResDto
{
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Token { get; set; } = String.Empty;
    public string RefreshToken { get; set; } = String.Empty;
}