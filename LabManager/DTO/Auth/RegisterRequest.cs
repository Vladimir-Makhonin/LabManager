using System.ComponentModel.DataAnnotations;

namespace LabManager.DTO.Auth
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}