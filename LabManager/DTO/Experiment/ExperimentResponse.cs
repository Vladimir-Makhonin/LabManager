namespace LabManager.DTO.Experiment
{
    public class ExperimentResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;


        public string? Description { get; set; }

        public DateTime Date { get; set; }

        public Guid PersonId { get; set; }
    }
}
