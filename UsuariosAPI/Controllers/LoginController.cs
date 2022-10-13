using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UsuariosAPI.Models;
using UsuariosAPI.Models.DTO;
using UsuariosAPI.Services;
using UsuariosAPI.Shared;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UsuarioService _service;
        RecaptchaValidate _recaptcha;

        public LoginController(UsuarioService service, RecaptchaValidate recaptcha)
        {
            _service = service;
            _recaptcha = recaptcha;
        }

        [HttpPost]
        public IActionResult LogarUsuario(LoginDto loginDto)
        {
            var resposta = _recaptcha.Validate(loginDto.recaptcha);
            if (resposta == true)
            {
                //Utilizando o pacote FluentResult
                Result resultado = _service.logarUsuario(loginDto);
                if (resultado.IsFailed)
                {
                    //não autorizado
                    return Unauthorized(resultado.Errors.FirstOrDefault());
                }

                return Ok(resultado.Successes.FirstOrDefault()); //retorna token para o client
            }
            return Unauthorized();

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
