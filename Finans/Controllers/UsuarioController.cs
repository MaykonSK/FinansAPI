using Finans.Models;
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

        [HttpPost("/api/upload/{userId}")]
        [Authorize(Roles = "admin, regular")]
        public async Task<string> UploadFileAsync([FromForm] IFormFile file, int userId)
        {
            return await _service.UploadFile(file, userId);
        }

        [HttpGet("/api/user/{id}")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult getUsuario(int id)
        {
            try
            {
                return Ok(_service.recuperarDadosUsuario(id));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{e.Message} \n {e.InnerException?.Message}");
            }
        }
    }
}
