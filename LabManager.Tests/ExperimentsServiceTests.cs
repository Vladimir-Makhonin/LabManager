using LabManager.Data;
using LabManager.DTO.Experiment;
using LabManager.Models;
using LabManager.Services;
using Microsoft.EntityFrameworkCore;

namespace LabManager.Tests
{
    public class ExperimentsServiceTests
    {
        [Fact]
        public async Task AddExperiment_NullRequest()
        {
            // Arrange
            DbContextOptions<LabManagerDbContext> options =
                new DbContextOptionsBuilder<LabManagerDbContext>()
                    .Options;

            using LabManagerDbContext dbContext =
                new LabManagerDbContext(options);

            ExperimentsService experimentsService =
                new ExperimentsService(dbContext);

            ExperimentAddRequest experimentAddRequest = null!;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => experimentsService.AddExperiment(
                    experimentAddRequest));
        }

        [Fact]
        public async Task AddExperiment_ValidRequest_SavesExperiment()
        {
            // Arrange
            DbContextOptions<LabManagerDbContext> options =
                new DbContextOptionsBuilder<LabManagerDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using LabManagerDbContext dbContext =
                new LabManagerDbContext(options);

            Person person = new Person
            {
                Id = Guid.NewGuid(),
                Name = "Test person",
                Email = "test@example.com"
            };

            dbContext.Persons.Add(person);

            await dbContext.SaveChangesAsync();

            ExperimentAddRequest request = new ExperimentAddRequest
            {
                Name = "Test experiment",
                Description = "Experiment created for a test",
                Date = DateTime.UtcNow,
                PersonId = person.Id
            };

            ExperimentsService experimentsService =
                new ExperimentsService(dbContext);

            // Act
            ExperimentResponse response =
                await experimentsService.AddExperiment(request);

            // Assert
            Experiment? savedExperiment =
                await dbContext.Experiments.FindAsync(response.Id);

            Assert.NotNull(savedExperiment);
            Assert.Equal(request.Name, savedExperiment.Name);
        }
    }
}