using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        UsuarioService _service;

        public LogoutController(UsuarioService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult DeslogarUsuario()
        {
            Result resultado = _service.deslogarUsuario();
            if (resultado.IsFailed)
            {
                return Unauthorized(resultado.Errors);
            }

            return Ok(resultado.Successes);
        }
    }
}
