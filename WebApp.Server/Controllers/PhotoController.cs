using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Services.Common;
using WebApp.Application.Services.Common.Models;

namespace WebApp.Server.Controllers
{
    [Route("/api/photo/")]
    public class PhotoController : Controller
    {
        private readonly GalleryService _galleryService;
        public PhotoController(GalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        [HttpGet("/api/image/{id:int}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {
            var image = await _galleryService.GetImageAsync(id);
            return File(image, "image/jpeg");
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _galleryService.GetPhotoAsync(id);
            return Ok(response);
        }
    }
}
