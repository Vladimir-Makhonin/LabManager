using LabManager.Contracts;
using LabManager.Data;
using LabManager.DTO.Experiment;
using LabManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManager.Services
{
    public class ExperimentsService : IExperimentsService
    {
        private readonly LabManagerDbContext _dbContext;

        public ExperimentsService(LabManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ExperimentResponse> AddExperiment(
            ExperimentAddRequest experimentAddRequest)
        {
            if (experimentAddRequest == null)
            {
                throw new ArgumentNullException(
                    nameof(experimentAddRequest));
            }

            if (experimentAddRequest.PersonId == Guid.Empty)
            {
                throw new ArgumentException(
                    "A valid person must be specified.");
            }

            bool personExists = await _dbContext.Persons
                .AnyAsync(person =>
                    person.Id == experimentAddRequest.PersonId);

            if (!personExists)
            {
                throw new ArgumentException(
                    "The specified person does not exist.");
            }

            Experiment experiment = new Experiment
            {
                Id = Guid.NewGuid(),
                Name = experimentAddRequest.Name!,
                Description = experimentAddRequest.Description,
                Date = experimentAddRequest.Date,
                PersonId = experimentAddRequest.PersonId
            };

            _dbContext.Experiments.Add(experiment);

            await _dbContext.SaveChangesAsync();

            return ConvertToExperimentResponse(experiment);
        }

        public async Task<List<ExperimentResponse>> GetAllExperiments()
        {
            List<Experiment> experiments =
                await _dbContext.Experiments
                    .AsNoTracking()
                    .ToListAsync();

            return experiments
                .Select(ConvertToExperimentResponse)
                .ToList();
        }

        public async Task<List<ExperimentResponse>> GetExperimentsByPerson(Guid personId)
        {
            if (personId == Guid.Empty)
            {
                throw new ArgumentException(
                    "A valid person identifier must be specified.");
            }

            List<Experiment> experiments =
                await _dbContext.Experiments
                    .AsNoTracking()
                    .Where(experiment =>
                        experiment.PersonId == personId)
                    .ToListAsync();

            return experiments
                .Select(ConvertToExperimentResponse)
                .ToList();
        }


        public async Task AssignEquipmentToExperiment(
    Guid experimentId,
    Guid equipmentId)
        {
            if (experimentId == Guid.Empty)
            {
                throw new ArgumentException(
                    "The experiment identifier is not valid.");
            }

            if (equipmentId == Guid.Empty)
            {
                throw new ArgumentException(
                    "The equipment identifier is not valid.");
            }

            bool experimentExists = await _dbContext.Experiments
                .AnyAsync(experiment => experiment.Id == experimentId);

            if (!experimentExists)
            {
                throw new ArgumentException(
                    "The specified experiment does not exist.");
            }

            bool equipmentExists = await _dbContext.Equipment
                .AnyAsync(equipment => equipment.Id == equipmentId);

            if (!equipmentExists)
            {
                throw new ArgumentException(
                    "The specified equipment does not exist.");
            }

            bool assignmentExists = await _dbContext.ExperimentEquipments
                .AnyAsync(experimentEquipment =>
                    experimentEquipment.ExperimentId == experimentId &&
                    experimentEquipment.EquipmentId == equipmentId);

            if (assignmentExists)
            {
                throw new ArgumentException(
                    "The equipment is already assigned to this experiment.");
            }

            ExperimentEquipment experimentEquipment =
                new ExperimentEquipment
                {
                    ExperimentId = experimentId,
                    EquipmentId = equipmentId
                };

            _dbContext.ExperimentEquipments.Add(experimentEquipment);

            await _dbContext.SaveChangesAsync();
        }

        private static ExperimentResponse ConvertToExperimentResponse(
            Experiment experiment)
        {
            return new ExperimentResponse
            {
                Id = experiment.Id,
                Name = experiment.Name,
                Description = experiment.Description,
                Date = experiment.Date,
                PersonId = experiment.PersonId
            };
        }
    }
}