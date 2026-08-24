using LabManager.DTO.Equipment;

namespace LabManager.Contracts
{
    public interface IEquipmentsService
    {
        Task<EquipmentResponse> AddEquipment(
            EquipmentAddRequest equipmentAddRequest);

        Task<List<EquipmentResponse>> GetAllEquipment();
    }
}