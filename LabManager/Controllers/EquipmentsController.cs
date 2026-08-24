using LabManager.Contracts;
using LabManager.DTO.Equipment;
using Microsoft.AspNetCore.Mvc;

namespace LabManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IEquipmentsService _equipmentsService;

        public EquipmentsController(
            IEquipmentsService equipmentsService)
        {
            _equipmentsService = equipmentsService;
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentResponse>> AddEquipment(
            EquipmentAddRequest equipmentAddRequest)
        {
            EquipmentResponse equipmentResponse =
                await _equipmentsService.AddEquipment(
                    equipmentAddRequest);

            return Ok(equipmentResponse);
        }

        [HttpGet]
        public async Task<ActionResult<List<EquipmentResponse>>>
            GetAllEquipment()
        {
            List<EquipmentResponse> equipment =
                await _equipmentsService.GetAllEquipment();

            return Ok(equipment);
        }
    }
}