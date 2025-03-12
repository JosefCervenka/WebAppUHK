using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Application.Services.Sys;
using WebApp.Core.Models.Recipe;
using WebApp.Infrastructure;

namespace WebApp.Server.Controllers;

[Route("/api/comment")]
public class CommentController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly SysUserService _sysUserService;

    public CommentController(AppDbContext context, SysUserService sysUserService)
    {
        _context = context;
        _sysUserService = sysUserService;
    }

    [Authorize]
    [HttpPost("/api/recipe/{recipeId:int}/comment")]
    public async Task<IActionResult> PostComment([FromRoute] int recipeId, [FromForm] string text,
        [FromForm] int rating)
    {
        var user = await _sysUserService.GetUserFromHttpContextAsync(HttpContext);

        _context.Comment.Add(new Comment()
        {
            RecipeId = recipeId,
            Author = user!,
            Text = text,
            Rating = rating
        });

        await _context.SaveChangesAsync();

        return Ok();
    }

    [Authorize]
    [HttpDelete("/api/comment/{id:int}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
        var user = await _sysUserService.GetUserFromHttpContextAsync(HttpContext);

        var comment = _context.Comment.FirstOrDefault(x => x.Id == id);

        if (comment is null)
        {
            return NotFound();
        }

        if (comment.AuthorId == user!.Id || user.UserSysRoles.Any(x => x.SysRole.Name == "Admin"))
        {
            await _context.Comment.Where(x => x.Id == id).ExecuteDeleteAsync();
            return Ok();
        }

        return Unauthorized();
    }
}