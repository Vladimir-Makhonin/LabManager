using Microsoft.AspNetCore.Identity;

namespace LabManager.Models
{
    /// <summary>
    /// Represents a user account that can authenticate in the application.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}