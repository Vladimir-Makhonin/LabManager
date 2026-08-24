using LabManager.Contracts;
using LabManager.Data;
using LabManager.DTO.Equipment;
using LabManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManager.Services
{
    public class EquipmentsService : IEquipmentsService
    {
        private readonly LabManagerDbContext _dbContext;

        public EquipmentsService(LabManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EquipmentResponse> AddEquipment(
            EquipmentAddRequest equipmentAddRequest)
        {
            if (equipmentAddRequest == null)
            {
                throw new ArgumentNullException(
                    nameof(equipmentAddRequest));
            }

            Equipment equipment = new Equipment
            {
                Id = Guid.NewGuid(),
                Name = equipmentAddRequest.Name!,
                Type = equipmentAddRequest.Type!,
                IsAvailable = equipmentAddRequest.IsAvailable
            };

            _dbContext.Equipment.Add(equipment);

            await _dbContext.SaveChangesAsync();

            return ConvertToEquipmentResponse(equipment);
        }

        public async Task<List<EquipmentResponse>> GetAllEquipment()
        {
            List<Equipment> equipment =
                await _dbContext.Equipment
                    .AsNoTracking()
                    .ToListAsync();

            return equipment
                .Select(ConvertToEquipmentResponse)
                .ToList();
        }

        private static EquipmentResponse ConvertToEquipmentResponse(
            Equipment equipment)
        {
            return new EquipmentResponse
            {
                Id = equipment.Id,
                Name = equipment.Name,
                Type = equipment.Type,
                IsAvailable = equipment.IsAvailable
            };
        }
    }
}