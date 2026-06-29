using Microsoft.AspNetCore.Mvc;
using LabManager.Services;
using LabManager.Models;


namespace LabManager.Controllers
{
    [ApiController]
    [Route("api/Greeting")]
    public class GreetingController : ControllerBase

    {

        public readonly GreetingService _greetingService;

        public GreetingController(GreetingService greetingService)

        {

            _greetingService = greetingService;

        }


        [HttpGet]
        public GreetingResponse GetGreeting()
        {
            GreetingResponse greetingResponse = new GreetingResponse();

            greetingResponse.Message = _greetingService.GetGreeting();

            return greetingResponse;

        }




    }
}
