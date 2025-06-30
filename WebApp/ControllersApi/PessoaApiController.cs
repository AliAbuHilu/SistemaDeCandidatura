using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaService _service;

        public PessoaController(PessoaService service)
        {
            _service = service;
        }





        [HttpGet]
        public IActionResult GetAll()
        {
            var pessoas = _service.Listar();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pessoa = _service.ObterPorId(id);
            if (pessoa == null) return NotFound();
            return Ok(pessoa);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PessoaViewModel pessoa)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _service.Criar(pessoa);
            return CreatedAtAction(nameof(Get), new { id = pessoa.Id }, pessoa);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PessoaViewModel pessoa)
        {
            if (id != pessoa.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _service.Atualizar(pessoa);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Excluir(id);
            return NoContent();
        }
    }
}
