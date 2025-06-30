using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.DTOs;
using AutoMapper;
using WebApp.Models;

namespace WebApp.Controllers.Api
{
    [ApiController]
    [Route("api/candidaturas")]
    public class CandidaturasApiController : ControllerBase
    {
        private readonly CandidaturaService _service;
        private readonly IMapper _mapper;

        public CandidaturasApiController(CandidaturaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/candidaturas
        [HttpGet]
        public IActionResult Get()
        {
            var candidaturas = _service.Listar();
            var dtos = _mapper.Map<List<CandidaturaDTO>>(candidaturas);
            return Ok(dtos);
        }

        // GET api/candidaturas/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var candidatura = _service.Listar().FirstOrDefault(c => c.Id == id);
            if (candidatura == null) return NotFound();

            var dto = _mapper.Map<CandidaturaDTO>(candidatura);
            return Ok(dto);
        }

        // POST api/candidaturas
        [HttpPost]
        public IActionResult Post([FromBody] CandidaturaDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var candidatura = _mapper.Map<Candidatura>(dto);
            _service.Adicionar(candidatura);

            return CreatedAtAction(nameof(Get), new { id = candidatura.Id }, candidatura);
        }

        // PUT api/candidaturas/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CandidaturaDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.Id) return BadRequest();

            var existente = _service.Listar().FirstOrDefault(c => c.Id == id);
            if (existente == null) return NotFound();

            var candidatura = _mapper.Map<Candidatura>(dto);
            _service.Atualizar(candidatura);

            return NoContent();
        }

        // DELETE api/candidaturas/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existente = _service.Listar().FirstOrDefault(c => c.Id == id);
            if (existente == null) return NotFound();

            _service.Remover(id);
            return NoContent();
        }

        // PATCH api/candidaturas/{id}/aprovar
        [HttpPatch("{id}/aprovar")]
        public IActionResult Aprovar(int id)
        {
            var candidatura = _service.Listar().FirstOrDefault(c => c.Id == id);
            if (candidatura == null) return NotFound();

            _service.Aprovar(id);
            return NoContent();
        }
    }
}
