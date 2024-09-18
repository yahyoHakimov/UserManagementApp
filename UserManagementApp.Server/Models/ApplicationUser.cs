using Microsoft.AspNetCore.Identity;

namespace UserManagementApp.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsBlocked { get; set; }
    }
}
