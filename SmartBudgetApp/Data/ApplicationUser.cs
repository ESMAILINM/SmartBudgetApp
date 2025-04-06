using Microsoft.AspNetCore.Identity;

namespace SmartBudgetApp.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public required string Role { get; set; } = string.Empty;

    }

}
