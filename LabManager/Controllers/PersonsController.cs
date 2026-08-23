using LabManager.DTO.Person;
using LabManager.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabManager.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {

        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]

        public async Task<ActionResult<PersonResponse>> AddPerson(
            PersonAddRequest personAddRequest)
        {
            PersonResponse personResponse =
                await _personService.AddPerson(personAddRequest);

            return Ok(personResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonResponse>> GetPersonById(Guid id)
        {
            PersonResponse? personResponse =
                await _personService.GetPersonById(id);

            if (personResponse == null)
            {
                return NotFound();
            }

            return Ok(personResponse);
        }


        [HttpGet]
        public async Task<ActionResult<List<PersonResponse>>> GetAllPersons()
        {
          List<PersonResponse> persons = await _personService.GetAllPersons();

            return Ok(persons);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PersonResponse>> UpdatePerson(
          Guid id,
          PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse =
                await _personService.UpdatePerson(id, personUpdateRequest);

            if (personResponse == null)
            {
                return NotFound();
            }

            return Ok(personResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            bool deleted = await _personService.DeletePerson(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
