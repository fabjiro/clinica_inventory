using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Auth
{
    public class AuthRefreshUserReqDto
    {
        [Required(ErrorMessage = "Refresh token is required")]
        [Display(Name = "RefreshToken")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}