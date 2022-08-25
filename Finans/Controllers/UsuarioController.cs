using Finans.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Finans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpPost("/api/upload")]
        [Authorize(Roles = "admin, regular")]
        public async Task<string> UploadFileAsync([FromForm] IFormFile file)
        {
            return await _service.UploadFile(file);
        }

        [HttpGet("/api/ImgProfile")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult GetFile()
        {
            //Byte[] b = System.IO.File.ReadAllBytes("Imagens/ImgUsers/imagem.jpg");   // You can use your own method over here.         
            //return File(b, "image/jpeg");

            try
            {
                var path = ("Imagens/ImgUsers/imagem.jpg");

                if (System.IO.File.Exists(path))
                {
                    const string contentType = "application/octet-stream";
                    return File(System.IO.File.OpenRead(path), contentType, Path.GetFileName(path));
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{e.Message} \n {e.InnerException?.Message}");
            }
        }
    }
}
