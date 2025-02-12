using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Sys;

namespace WebApp.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetUserWithRolesAsync(int id)
        {
            return await _context.User
                .Include(x => x.UserSysRoles)
                .ThenInclude(x => x.SysRole)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetByNameAsync(string name)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
