using System.ComponentModel.DataAnnotations;

namespace LabManager.DTO.Equipment
{
    public class EquipmentAddRequest
    {
        [Required(ErrorMessage = "El nombre del equipo es obligatorio")]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "El tipo de equipo es obligatorio")]
        [StringLength(100)]
        public string? Type { get; set; }

        public bool IsAvailable { get; set; }
    }
}