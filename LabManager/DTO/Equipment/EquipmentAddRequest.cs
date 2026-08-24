using System.ComponentModel.DataAnnotations;

namespace LabManager.DTO.Equipment
{
    public class EquipmentAddRequest
    {
        [Required(ErrorMessage = "Name of the experiment is required")]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The type of equipment is required")]
        [StringLength(100)]
        public string? Type { get; set; }

        public bool IsAvailable { get; set; }
    }
}