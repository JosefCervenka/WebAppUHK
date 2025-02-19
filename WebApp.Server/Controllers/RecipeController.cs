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

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string title, [FromForm] string description, [FromForm] IFormFile picture, [FromForm] List<string> test)
        {
            
            long length = picture.Length;
            if (length < 0)
                return BadRequest();

            using var fileStream = picture.OpenReadStream();
            byte[] bytes = new byte[length];
            fileStream.Read(bytes, 0, (int)picture.Length);
            
            
            return Ok();
        }
    }
}