using Finans.DTO;
using Finans.Models;
using Finans.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Finans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasPagarController : ControllerBase
    {
        ContasPagarService _service;

        public ContasPagarController(ContasPagarService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult RecuperarContas([FromRoute] int userId)
        {
            return Ok(_service.recuperarContas(userId));
        }

        [HttpPost]
        [Authorize(Roles = "admin, regular")]
        public IActionResult CadastrarConta([FromBody] ContasPagar contapagar)
        {
            Result resultado = _service.cadastrarConta(contapagar);

            if (resultado.IsSuccess)
            {
                return Ok(resultado.Successes.FirstOrDefault());
            }
            return BadRequest(resultado.IsFailed);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult DeletarConta([FromRoute] int id)
        {
            Result resultado = _service.deletarConta(id);

            if (resultado.IsSuccess)
            {
                return Ok(resultado.Successes.FirstOrDefault());
            }

            return NotFound(resultado.IsFailed);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult atualizarConta(int id, ContasPagarDto contapagar)
        {
            Result resultado = _service.atualizarConta(id, contapagar);

            if (resultado.IsSuccess)
            {
                return Ok(resultado.Successes.FirstOrDefault());
            }

            return NotFound(resultado.IsFailed);
        }

        [HttpPut("/api/contapaga/{id}")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult ContaPaga(int id, ContaPagaDto contaPaga)
        {
            Result resultado = _service.contaPaga(id, contaPaga);

            if (resultado.IsSuccess)
            {
                return Ok(resultado.Successes.FirstOrDefault());
            }

            return NotFound(resultado.IsFailed);
        }

        [HttpGet("/api/totalconta/{userId}")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult ContaTotal([FromRoute] int userId)
        {
            return Ok(_service.totalContas(userId));
        }
    }
}
