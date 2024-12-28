using Microsoft.AspNetCore.Identity;

namespace kuafor.api.Models.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
    }
}
