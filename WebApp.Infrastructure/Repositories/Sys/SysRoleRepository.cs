using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Sys;
using WebApp.Infrastructure.Repositories.Base;

namespace WebApp.Infrastructure.Repositories.Sys
{
    public class SysRoleRepository : Repository<SysRole>
    {
        public SysRoleRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<SysRole>> GetByUserId(int userId)
        {
            var roles = await _context.UserSysRole
                .Include(x => x.SysRole)
                    .Where(x => x.UserId == userId)
                .Select(x => x.SysRole).ToListAsync();

            return roles;
        }

        public async Task AddUserSysRole(User user, int sysRoleId)
        {
            _context.UserSysRole.Add(new UserSysRole
            {
                User = user,
                SysRoleId = sysRoleId
            });

            await _context.SaveChangesAsync();
        }

        public async Task AddUserSysRole(int userId, int sysRoleId)
        {
            _context.UserSysRole.Add(new UserSysRole
            {
                UserId = userId,
                SysRoleId = sysRoleId
            });

            await _context.SaveChangesAsync();
        }
    }
}
