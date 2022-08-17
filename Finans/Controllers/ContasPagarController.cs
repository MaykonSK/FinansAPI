using Finans.Models;
using Finans.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]
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
                return Ok();
            }
            return BadRequest(resultado.ToString());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult DeletarConta([FromRoute] int id)
        {
            Result resultado = _service.deletarConta(id);

            if (resultado.IsSuccess)
            {
                return Ok();
            }

            return NotFound(resultado.ToString());
        }
    }
}
