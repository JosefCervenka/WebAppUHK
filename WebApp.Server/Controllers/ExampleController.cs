using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Enums;
using WebApp.Core.Models.Sys;

namespace WebApp.Server.Controllers
{
    [Route("/api/example/")]
    public class ExampleController : ControllerBase
    {
        [Authorize]
        [HttpGet("authorized")]
        public IActionResult Authorized()
        {
            return Ok("Only for authorized users");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("role")]
        public IActionResult Role()
        {
            return Ok("Only for admins");
        }
    }
}
