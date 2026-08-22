using System.ComponentModel.DataAnnotations;

namespace LabManager.DTO.Experiment
{
    public class ExperimentUpdateRequest
    {
        [Required(ErrorMessage = "El nombre del experimento es obligatorio")]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime Date { get; set; }

        public Guid PersonId { get; set; }
    }
}