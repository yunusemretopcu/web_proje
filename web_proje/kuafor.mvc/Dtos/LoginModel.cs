using System.ComponentModel.DataAnnotations;

namespace kuafor.mvc.Dtos
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
