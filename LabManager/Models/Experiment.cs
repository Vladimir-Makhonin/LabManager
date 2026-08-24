namespace LabManager.Models
{
public class Experiment
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public Guid PersonId { get; set; }

    public Person Person { get; set; } = null!;

    public ICollection<ExperimentEquipment> ExperimentEquipments
    { get; set; } = new List<ExperimentEquipment>();
}
}