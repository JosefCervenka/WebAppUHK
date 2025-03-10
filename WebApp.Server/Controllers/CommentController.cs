using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> PostComment([FromRoute] int recipeId, [FromForm] string text, [FromForm] int rating)
    {
        var user = await _sysUserService.GetUserFromHttpContextAsync(HttpContext);
        
        _context.Comment.Add(new Comment()
        {
            RecipeId = recipeId,
            Author = user!,
            Text =  text,
            Rating = rating
        });

        await _context.SaveChangesAsync(); 

        return Ok();
    }
}