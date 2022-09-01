using Finans.DTO;
using Finans.Models;
using Finans.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Finans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImoveisController : ControllerBase
    {
        ImoveisService _service;

        public ImoveisController(ImoveisService service)
        {
            _service = service;
        }

        // GET api/<ImoveisController>/5
        [HttpGet("{userId}")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult Get(int userId)
        {
            return Ok(_service.recuperarImoveis(userId));
        }

        // POST api/<ImoveisController>
        [HttpPost]
        [Authorize(Roles = "admin, regular")]
        public IActionResult Post([FromBody] PostImovelDTO value)
        {
            Result resultado = _service.cadastrarImovel(value);

            if (resultado.IsSuccess)
            {
                return Ok(resultado.Successes.FirstOrDefault());
            }
            return BadRequest(resultado.IsFailed);

        }

        // PUT api/<ImoveisController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE api/<ImoveisController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
