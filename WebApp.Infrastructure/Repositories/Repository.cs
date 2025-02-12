using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models;
using WebApp.Core.Models.Base;

namespace WebApp.Infrastructure.Repositories
{
    public class Repository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<ICollection<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> GetAsync(int id)
        {
            return await _context.Set<TEntity>()
                .FirstOrDefaultAsync(X => X.Id == id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);

            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            TEntity? entity = await GetAsync(id);

            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
