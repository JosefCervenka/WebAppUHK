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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var recipe = await _context.Recipe.FirstOrDefaultAsync(x => x.Id == id);

            if (recipe is null)
                return NotFound();

            var user = await _sysUserService.GetUserFromHttpContextAsync(HttpContext);

            if (recipe.AuthorId == user!.Id || user.UserSysRoles.Any(x => x.SysRole.Name == "Admin"))
            {
                await _context.Recipe.Where(x => x.Id == id).ExecuteDeleteAsync();

                return Ok();
            }

            return Unauthorized();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var recipe = await _context.Recipe
                .Include(x => x.Author)
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .Include(x => x.HeaderPhoto)
                .Include(x => x.Steps)
                .Include(x => x.Ingredients)
                .ThenInclude(x => x.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);

            recipe?.Steps?.ForEach(x => x.Recipe = null);
            recipe?.Ingredients?.ForEach(x => x.Recipe = null);
            recipe?.Comments?.ForEach(x => x.Recipe = null);

            return Ok(recipe);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null)
        {
            List<Recipe> recipes = null;

            if (!string.IsNullOrEmpty(search))
            {
                recipes = await _context.Recipe
                    .FromSqlRaw("""
                                SELECT * FROM "Recipe" WHERE "Title" % {0} ORDER BY similarity("Title", {0}) DESC
                                """, search)
                    .Include(x => x.HeaderPhoto)
                    .Include(x => x.Author)
                    .Include(x => x.Comments)
                    .ToListAsync();
            }
            else
            {
                recipes = await _context.Recipe
                    .Include(x => x.HeaderPhoto)
                    .Include(x => x.Author)
                    .Include(x => x.Comments)
                    .ToListAsync();
            }


            recipes.ForEach(x => x.Comments.ForEach(y => y.Recipe = null));

            return Ok(recipes);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] string title, [FromForm] string text,
            [FromForm] IFormFile picture,
            [FromForm] List<string> steps, [FromForm] List<string> ingredients, [FromForm] List<int> unitIds,
            [FromForm] List<int> counts)
        {
            var image = await _imageService.CreateImageAsync(picture);

            if (ingredients is null or [] || unitIds is null or [] || counts is null or [])
                return BadRequest(new
                {
                    Message = "Ingredients cannot be empty. Add at least one step!"
                });

            if (ingredients.Count != unitIds.Count && ingredients.Count != counts.Count)
                return BadRequest(new
                {
                    Message = "Error"
                });

            if (steps is null or [])
                return BadRequest(new
                {
                    Message = "Steps cannot be empty. Add at least one step!"
                });


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
                Author = (await _sysUserService.GetUserFromHttpContextAsync(HttpContext))!,
                Title = title,
                Text = text,
                HeaderPhoto = new Photo()
                {
                    Name = picture.Name,
                    Image = image.image,
                },
                Steps = steps.Select(x => new Step
                {
                    Text = x,
                }).ToList(),
                Ingredients = []
            };

            for (var i = 0; i < ingredients.Count; i++)
            {
                var ingredient = ingredients[i];
                var unitId = unitIds[i];
                var count = counts[i];

                recipe.Ingredients.Add(new Ingredient()
                {
                    Name = ingredient,
                    Count = count,
                    UnitId = unitId
                });
            }

            await _context.Recipe.AddAsync(recipe);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Id = recipe.Id,
            });
        }
    }
}