using Microsoft.AspNetCore.Mvc;
using LabManager.Services;


namespace LabManager.Controllers
{
    [ApiController]
    [Route("api/Greeting")]
    public class GreetingController : ControllerBase

    {

        public readonly GreetingService _greetingService;


      
     
        [HttpGet]
        public string GetGreeting()
        {
            string respuesta = _greetingService.GetGreeting();

            return respuesta;
        }



        public GreetingController(GreetingService greetingService) 
        
        {

            _greetingService = greetingService;
        
        }
    }
}
