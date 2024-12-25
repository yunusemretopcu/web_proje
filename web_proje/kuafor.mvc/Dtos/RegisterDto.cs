using System.ComponentModel.DataAnnotations;

namespace kuafor.mvc.Dtos
{
    public record RegisterDto
    {
        [Required(ErrorMessage = "User Name is required.")]
        public String? UserName { get; init; }

        [Required(ErrorMessage = "Email is required.")]
        public String? Email { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public String? Password { get; init; }
    }
}
