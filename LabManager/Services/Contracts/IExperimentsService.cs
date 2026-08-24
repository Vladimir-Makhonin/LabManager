using LabManager.DTO.Experiment;

namespace LabManager.Contracts
{
    public interface IExperimentsService
    {
        Task<ExperimentResponse> AddExperiment(
            ExperimentAddRequest experimentAddRequest);

        Task<List<ExperimentResponse>> GetAllExperiments();
    

    Task<List<ExperimentResponse>> GetExperimentsByPerson(
            Guid personId);



        Task AssignEquipmentToExperiment(
    Guid experimentId,
    Guid equipmentId);
    }

}