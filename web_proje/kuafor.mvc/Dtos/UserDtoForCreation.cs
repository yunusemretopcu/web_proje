using System.ComponentModel.DataAnnotations;

namespace kuafor.mvc.Dtos
{
    public record UserDtoForCreation : UserDto
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public String? Password { get; init; }
    }
}
