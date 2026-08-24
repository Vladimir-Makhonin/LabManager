namespace LabManager.Models
{
    public class ExperimentEquipment
    {
        public Guid ExperimentId { get; set; }

        public Experiment Experiment { get; set; } = null!;

        public Guid EquipmentId { get; set; }

        public Equipment Equipment { get; set; } = null!;
    }
}
