using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Application.Services.Sys;
using WebApp.Application.Services.Sys.Models;
using WebApp.Infrastructure;

namespace WebApp.Server.Controllers
{
    [Controller]
    public class LoginController : ControllerBase
    {
        private SysUserService _sysUserService { get; set; }
        public LoginController(SysUserService sysUserService)
        {
            _sysUserService = sysUserService;
        }

        [HttpGet("/api/unauthorized")]
        public async Task<IActionResult> Unuthorized()
        {
            return BadRequest("You are unauthorized");
        }

        [HttpGet("/api/no-login")]
        public async Task<IActionResult> NotLogin()
        {
            return BadRequest("You are not login in");
        }

        [HttpPost("/api/login")]
        public async Task<IActionResult> LoginAsync([FromBody] SysUserLoginDTO sysUserLogin)
        {
            var (token, message) = await _sysUserService.LoginUserAsync(sysUserLogin);

            if (token is not null)
            {
                HttpContext.Response.Cookies.Append("authorization", token);
                return Ok(message);
            }

            return BadRequest(message);
        }

        [HttpPost("/api/register")]
        public async Task<IActionResult> RegisterAsync([FromBody] SysUserRegiterDTO sysUserRegiter)
        {
            await _sysUserService.RegisterUserAsync(sysUserRegiter);

            return Ok();
        }
    }
}
