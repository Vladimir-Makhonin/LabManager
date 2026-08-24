using LabManager.Contracts;
using LabManager.DTO.Experiment;
using Microsoft.AspNetCore.Mvc;

namespace LabManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperimentsController : ControllerBase
    {
        private readonly IExperimentsService _experimentsService;

        public ExperimentsController(
            IExperimentsService experimentsService)
        {
            _experimentsService = experimentsService;
        }

        [HttpPost]
        public async Task<ActionResult<ExperimentResponse>> AddExperiment(
            ExperimentAddRequest experimentAddRequest)
        {
            ExperimentResponse experimentResponse =
                await _experimentsService.AddExperiment(
                    experimentAddRequest);

            return Ok(experimentResponse);
        }

        [HttpGet]
        public async Task<ActionResult<List<ExperimentResponse>>>
            GetAllExperiments()
        {
            List<ExperimentResponse> experiments =
                await _experimentsService.GetAllExperiments();

            return Ok(experiments);
        }

        [HttpGet("person/{personId:guid}")]
        public async Task<ActionResult<List<ExperimentResponse>>>
            GetExperimentsByPerson(Guid personId)
        {
            List<ExperimentResponse> experiments =
                await _experimentsService.GetExperimentsByPerson(
                    personId);

            return Ok(experiments);
        }

        [HttpPost("{experimentId:guid}/equipment/{equipmentId:guid}")]
        public async Task<IActionResult> AssignEquipmentToExperiment(
    Guid experimentId,
    Guid equipmentId)
        {
            await _experimentsService.AssignEquipmentToExperiment(
                experimentId,
                equipmentId);

            return NoContent();
        }
    }
}
