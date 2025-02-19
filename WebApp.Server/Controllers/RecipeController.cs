using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Infrastructure;

namespace WebApp.Server.Controllers
{
    [Route("api/recipe")]
    public class RecipeController : ControllerBase
    {
        private AppDbContext _context;

        public RecipeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var recepie = await _context.Recipe
                .Include(x => x.Comments)
                .Include(x => x.HeaderPhoto)
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(recepie);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recepie = await _context.Recipe
                .Include(x => x.HeaderPhoto)
                .ToListAsync();

            return Ok(recepie);
        }
    }
}
