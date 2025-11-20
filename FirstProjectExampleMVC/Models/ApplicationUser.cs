using Microsoft.AspNetCore.Identity;

namespace FirstProjectExampleMVC.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Role { get; set; } = "User"; 

    }
}
