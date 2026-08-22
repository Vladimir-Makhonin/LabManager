using System.ComponentModel.DataAnnotations;


namespace LabManager.DTO.Experiment
{
    public class ExperimentAddRequest
    {
        [Required(ErrorMessage = "El nombre del experimento es obligatorio")]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid PersonId { get; set; }
    }
}