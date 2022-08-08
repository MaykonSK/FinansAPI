using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Models;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UsuarioService _service;
        public LoginController(UsuarioService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult LogarUsuario(Login login)
        {
            //Utilizando o pacote FluentResult
            Result resultado = _service.logarUsuario(login);
            if (resultado.IsFailed)
            {
                //não autorizado
                return Unauthorized(resultado.Errors.FirstOrDefault());
            }

            return Ok(resultado.Successes.FirstOrDefault()); //retorna token para o client
        }

        [HttpPost("/solicita-reset-senha")]
        public IActionResult SolicitaResetSenha(SolicitaResetSenha request)
        {
            Result resultado = _service.solicitaResetSenha(request);
            if (resultado.IsFailed)
            {
                return Unauthorized(resultado.Errors);
            }

            return Ok(resultado.Successes.FirstOrDefault());
        }

        [HttpPost("/redefinir-senha")]
        public IActionResult RedefinirSenha(RedefinicaoSenha request)
        {
            Result resultado = _service.redefinirSenha(request);
            if (resultado.IsFailed)
            {
                return Unauthorized(resultado.Errors);
            }

            return Ok(resultado.Successes.FirstOrDefault());
        }
    }
}
