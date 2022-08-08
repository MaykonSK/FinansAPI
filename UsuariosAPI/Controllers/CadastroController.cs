using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Models;
using UsuariosAPI.Models.DTO;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        UsuarioService _service;
        public CadastroController(UsuarioService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult cadastrarUsuario(CreateUsuarioDto createDto)
        {
            Result resultado = _service.cadastrarUsuario(createDto);
            if (resultado.IsFailed)
            {
                return StatusCode(500);
            }
            return Ok(resultado.Successes.FirstOrDefault());
        }

        [HttpGet("/ativa")]
        public IActionResult ativaContaUsuario([FromQuery] AtivaConta request)
        {
            Result resultado = _service.ativaContaUsuario(request);

            if (resultado.IsFailed)
            {
                return StatusCode(500);
            }

            return Ok(resultado.Successes.FirstOrDefault());
        }
    }
}
