using LabManager.Data;
using LabManager.DTO.Person;
using LabManager.Models;
using LabManager.Services.Contracts;
using Microsoft.EntityFrameworkCore;



namespace LabManager.Services
{
    public class PersonService : IPersonService
    {


        private readonly LabManagerDbContext _dbContext;


        public PersonService(LabManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Adds a new person to the database.
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns></returns>
        public async Task<PersonResponse> AddPerson(PersonAddRequest personAddRequest)
        {
            Person person = new Person
            {
                Id = Guid.NewGuid(),
                Name = personAddRequest.Name,
                Email = personAddRequest.Email
            };

            _dbContext.Persons.Add(person);

            await _dbContext.SaveChangesAsync();

            return new PersonResponse
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email
            };
        }

        public async Task<bool> DeletePerson(Guid id)
        {
            Person? person = await _dbContext.Persons
        .FirstOrDefaultAsync(person => person.Id == id);

            if (person == null)
            {
                return false;
            }

            _dbContext.Persons.Remove(person);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Retrieves all persons from the database and returns them as a list of PersonResponse objects.
        /// </summary>
        /// <returns></returns>
        public async Task<List<PersonResponse>> GetAllPersons()
        {
            List<Person> persons = await _dbContext.Persons.ToListAsync();

            return persons.Select(person => new PersonResponse
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email
            }).ToList();
        }


        /// <summary>
        /// Retrieves a person by their ID from the database and returns it as a PersonResponse object. If the person is not found, returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PersonResponse?> GetPersonById(Guid id)
        {
            Person? person = await _dbContext.Persons
        .FirstOrDefaultAsync(person => person.Id == id);

            if (person == null)
            {
                return null;
            }

            return new PersonResponse
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email
            };
        }


        /// <summary>
        /// Updates an existing person's information in the database based on their ID. If the person is not found, returns null. Otherwise, updates the person's name and email and returns the updated information as a PersonResponse object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personUpdateRequest"></param>
        /// <returns></returns>
        public async Task<PersonResponse?> UpdatePerson(Guid id, PersonUpdateRequest personUpdateRequest)
        {
            Person? person = await _dbContext.Persons
        .FirstOrDefaultAsync(person => person.Id == id);

            if (person == null)
            {
                return null;
            }

            person.Name = personUpdateRequest.Name;
            person.Email = personUpdateRequest.Email;

            await _dbContext.SaveChangesAsync();

            return new PersonResponse
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email
            };
        }
    }
}
