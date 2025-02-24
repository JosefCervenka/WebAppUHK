using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Application.Services.Sys;
using WebApp.Application.Services.Sys.Models;
using WebApp.Core.Models.Sys;
using WebApp.Infrastructure;

namespace WebApp.Server.Controllers
{
    [Controller]
    [Route("/api/authorization/")]
    public class AuthorizationController : ControllerBase
    {
        private readonly SysUserService _sysUserService;

        public AuthorizationController(SysUserService sysUserService)
        {
            _sysUserService = sysUserService;
        }

        [HttpGet("/api/user/{id:int}")]
        public async Task<IActionResult> getSimpleUserInfo(int id)
        {
            var user = await _sysUserService.GetUserByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(new
            {
                Name = user.Name,
            });
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var user = await _sysUserService.GetUserFromHttpContextAsync(HttpContext);
                return Ok(new
                {
                    Name = user!.Name,
                    Email = user!.Email,
                    UserRoles = user.UserSysRoles.Select(x => x.SysRole.Name).ToList()
                });
            }
            catch (Exception)
            {
                return NotFound(new { message = "User was not found." });
            }
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
            var user = await _sysUserService.RegisterUserAsync(sysUserRegiter);

            if (user.errorMessage is not null)
            {
                return BadRequest(new { message = user.errorMessage });
            }

            return Ok(user);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            HttpContext.Response.Cookies.Delete("authorization");

            return Ok(new
            {
                Message = "You are logged out."
            });
        }
    }
}