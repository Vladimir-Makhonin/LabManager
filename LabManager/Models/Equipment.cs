namespace LabManager.Models
{
    public class Equipment
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }

        public ICollection<ExperimentEquipment> ExperimentEquipments
        { get; set; } = new List<ExperimentEquipment>();
    }
}