using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using WebApp.Application.Services.Common;
using WebApp.Application.Services.Sys;
using WebApp.Infrastructure;
using WebApp.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<JWTClaimMiddleWare>();

builder.Services.AddScoped<SysUserService>();
builder.Services.AddScoped<GalleryService>();


builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/authorization/no-login";
        options.AccessDeniedPath = "/api/authorization/unauthorized";
    });

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<JWTClaimMiddleWare>();

app.UseAuthorization();


app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
