using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("welcome")]
    public class WelcomeController : ControllerBase
    {
        public ActionResult welcome()
        {
            return Ok();
        }
    }
}