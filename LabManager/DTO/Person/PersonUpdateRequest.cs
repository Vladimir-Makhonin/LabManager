using System.ComponentModel.DataAnnotations;

namespace LabManager.DTO.Person
{
    public class PersonUpdateRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string? Email { get; set; }
    }
}