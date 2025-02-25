using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApp.Application.Services.Sys;
using WebApp.Application.Utils;
using System.Security.Claims;
namespace WebApp.Server.Middlewares
{
    public class JwtClaimMiddleWare : IMiddleware
    {
        SysUserService _sysUserService;
        public JwtClaimMiddleWare(SysUserService sysUserService)
        {
            _sysUserService = sysUserService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Cookies["authorization"];

            if (token is not null)
            {
                context.User = _sysUserService.GetClaimsFromToken(token);
            }
            
            await next.Invoke(context);
        }
    }
}
