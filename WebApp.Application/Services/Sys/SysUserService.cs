using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebApp.Application.Services.Sys.Models;
using WebApp.Application.Utils;
using WebApp.Core.Enums;
using WebApp.Core.Models.Sys;
using WebApp.Infrastructure;
using WebApp.Infrastructure.Repositories.Sys;

namespace WebApp.Application.Services.Sys
{
    public class SysUserService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly UserRepository _userRepository;
        private readonly SysRoleRepository _sysRoleRepository;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtGenerator _jwtGenerator;

        private readonly string _tokenKey;
        private readonly string _issuer;
        private readonly string _audience;


        public SysUserService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
            _sysRoleRepository = new SysRoleRepository(_context);
            _passwordHasher = new PasswordHasher();
            _jwtGenerator = new JwtGenerator();
            _configuration = configuration;

            _tokenKey = configuration["JWT:WebTokenKey"]!;
            _issuer = configuration["JWT:Issuer"]!;
            _audience = configuration["JWT:Audience"]!;
        }

        public ClaimsPrincipal GetClaimsFromToken(string token) =>
            _jwtGenerator.ValidateJwtToken(token, _tokenKey, _issuer, _audience);
        
        public async Task<User?> GetUserFromHttpContextAsync(HttpContext httpContext)
        {
            var name = httpContext.User?.Identity?.Name;

            var id = await _context.User
                .Where(x => x.Name == name)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return await _userRepository.GetUserWithRolesAsync(id);
        }
        public async Task<(string? token, string StatusMessage)> LoginUserAsync(SysUserLoginDTO userDTO)
        {
            User user = await _userRepository.GetByEmailAsync(userDTO.Email.ToLower());

            if (user == null)
            {
                return (null, "There is no user with this email.");
            }

            var validUser = _passwordHasher.VerifyPassword(userDTO.Password, user.Salt, user.PasswordHash);

            if (validUser)
            {
                user = await _userRepository.GetUserWithRolesAsync(user.Id);

                var token = _jwtGenerator.GenerateJwtToken(_tokenKey, _issuer, _audience, user);

                return (token, "User successfully logged in.");
            }

            return (null, "Invalid password entered.");
        }

        public async Task<User> RegisterUserAsync(SysUserRegiterDTO userDTO)
        {
            var hash = _passwordHasher.HashPassword(userDTO.Password, out byte[] salt);

            User user = new User()
            {
                Email = userDTO.Email.ToLower(),
                Name = userDTO.Name,
                PasswordHash = hash,
                Salt = salt
            };

            await _userRepository.AddAsync(user);

            await _sysRoleRepository.AddUserSysRole(user, (int)SysRoleEnum.User);

            return user;
        }
    }
}