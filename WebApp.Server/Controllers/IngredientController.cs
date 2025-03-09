using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Services.Common;
using WebApp.Core.Models.Recipe;
using WebApp.Infrastructure;
using WebApp.Infrastructure.Repositories.Base;

namespace WebApp.Server.Controllers
{
    [Route("/api/ingredient")]
    public class IngredientController : ControllerBase
    {
        private readonly Repository<Unit> _unitRepository;
        private readonly AppDbContext _context;

        public IngredientController(AppDbContext context)
        {
            _context = context;
            _unitRepository = new Repository<Unit>(_context);
        }

        [Route("/api/unit")]
        public async Task<IActionResult> GetUnits()
        {
            return Ok(await _unitRepository.GetAll());
        }
    }
}