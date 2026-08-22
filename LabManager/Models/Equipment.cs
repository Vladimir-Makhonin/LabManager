namespace LabManager.Models
{
    public class Equipment
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Type { get; set; }

        public bool IsAvailable { get; set; }
    }
}
