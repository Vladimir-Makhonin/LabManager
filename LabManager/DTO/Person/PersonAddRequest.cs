using System.ComponentModel.DataAnnotations;

namespace LabManager.DTO.Person
{
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
