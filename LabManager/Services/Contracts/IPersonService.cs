using LabManager.DTO.Person;

namespace LabManager.Services.Contracts
{
    public interface IPersonService
    {
        Task<PersonResponse> AddPerson(PersonAddRequest personAddRequest);

        Task<List<PersonResponse>> GetAllPersons();

        Task<PersonResponse?> GetPersonById(Guid id);

        Task<PersonResponse?> UpdatePerson(Guid id, PersonUpdateRequest personUpdateRequest);

        Task<bool> DeletePerson(Guid id);
    }
}