using Microsoft.AspNetCore.Identity;

namespace kuafor.mvc.Models.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
    }
}
