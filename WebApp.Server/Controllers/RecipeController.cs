using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Application.Services.Common;
using WebApp.Application.Services.Sys;
using WebApp.Core.Models.Common;
using WebApp.Core.Models.Recipe;
using WebApp.Infrastructure;

namespace WebApp.Server.Controllers
{
    [Route("api/recipe")]
    public class RecipeController : ControllerBase
    {
        private AppDbContext _context;
        private SysUserService _sysUserService;
        private ImageService _imageService;

        public RecipeController(AppDbContext context, SysUserService sysUserService, ImageService imageService)
        {
            _context = context;
            _sysUserService = sysUserService;
            _imageService = imageService;
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
        [Authorize]
        public async Task<IActionResult> Post([FromForm] string title, [FromForm] string text, [FromForm] IFormFile picture)
        {
            var image = await _imageService.CreateImageAsync(picture);

            if (image.image == null)
                return BadRequest(new
                {
                    Message = image.message
                });

            if (string.IsNullOrEmpty(title))
                return BadRequest(new
                {
                    Message = "Title cannot be empty!"
                });

            if (string.IsNullOrEmpty(text))
                return BadRequest(new
                {
                    Message = "Text cannot be empty!"
                });

            var recipe = new Recipe()
            {
                Author = await _sysUserService.GetUserFromHttpContextAsync(HttpContext),
                Title = title,
                Text = text,
                HeaderPhoto = new Photo()
                {
                    Name = picture.Name,
                    Image = image.image,
                },
            };

            await _context.Recipe.AddAsync(recipe);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}