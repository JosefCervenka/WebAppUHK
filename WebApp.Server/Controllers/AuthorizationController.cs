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
    [Route("/api/authorization/")]
    public class AuthorizationController : ControllerBase
    {
        private SysUserService _sysUserService { get; set; }

        public AuthorizationController(SysUserService sysUserService)
        {
            _sysUserService = sysUserService;
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var user = await _sysUserService.GetUserFromHttpContextAsync(HttpContext);
            return Ok(user);
        }

        [HttpGet("unauthorized")]
        public async Task<IActionResult> Unuthorized()
        {
            return BadRequest(new
            {
                Message = "You are unauthorized."
            });
        }

        [HttpGet("no-login")]
        public async Task<IActionResult> NotLogin()
        {
            return BadRequest(new
            {
                Message = "You are not login in."
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] SysUserLoginDTO sysUserLogin)
        {
            var (token, message) = await _sysUserService.LoginUserAsync(sysUserLogin);
            
            if (token is not null)
            {
                HttpContext.Response.Cookies.Append("authorization", token);
                return Ok(new
                {
                    Message = message
                });
            }

            return BadRequest(new
            {
                Message = message
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] SysUserRegiterDTO sysUserRegiter)
        {
            await _sysUserService.RegisterUserAsync(sysUserRegiter);
            return Ok(new
            {
                Message = "Your account was successfully created."
            });
        }
    }
}