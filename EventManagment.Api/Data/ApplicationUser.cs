using Microsoft.AspNetCore.Identity;

namespace EventManagment.Api.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string fullname { get; set; } = string.Empty;
    }
}
