using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Services.Common;
using WebApp.Application.Services.Common.Models;
using WebApp.Application.Services.Sys;
using WebApp.Core.Models.Common;

namespace WebApp.Server.Controllers
{
    [Route("/api/gallery")]
    public class GalleryController : ControllerBase
    {
        private readonly GalleryService _galleryService;
        private readonly SysUserService _sysUserService;
        public GalleryController(GalleryService galleryService, SysUserService sysUserService)
        {
            _galleryService = galleryService;
            _sysUserService = sysUserService;
        }

        [Authorize]
        public async Task<IActionResult> Post([FromBody] GalleryDTO gallery)
        {
            var user = await _sysUserService.GetUserFromHttpContextAsync(HttpContext);
            await _galleryService.AddGalleryAsync(gallery, user!.Id);
            return Ok();
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var gallery = await _galleryService.GetGalleryAsync(id);

            if (gallery is null)
                return BadRequest("Gallery does not exist.");

            return Ok(gallery);
        }

        [Authorize]
        [HttpPost("{id:int}/image")]
        public async Task<IActionResult> Post([FromBody] PhotoBase64DTO photo, [FromRoute] int id)
        {
            await _galleryService.AddPhotoAsync(photo, id);
            return Ok();
        }
    }
}
