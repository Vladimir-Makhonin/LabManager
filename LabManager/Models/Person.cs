using LabManager.Models;

public class Person
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public ICollection<Experiment> Experiments { get; set; }
        = new List<Experiment>();
}