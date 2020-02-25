using Microsoft.AspNetCore.Mvc;

namespace IdCardReaderApi.Controllers
{
    [Route("")]
    [Route("welcome")]
    public class WelcomeController : ControllerBase
    {
        public string Welcome()
        {
            return "Welcome to ID Card Reader API";
        }
    }
}