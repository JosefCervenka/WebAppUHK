using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Services.Common;
using WebApp.Application.Services.Common.Models;

namespace WebApp.Server.Controllers
{
    [Route("/api/image")]
    public class ImageController : Controller
    {
        private readonly ImageService _imageService;
        public ImageController(ImageService galleryService)
        {
            _imageService = galleryService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {
            var image = await _imageService.GetImageAsync(id);

            if (image is null)
                return NotFound("Image does not exist.");

            return File(image.Data, $"{image.Type}");
        }
    }
}
