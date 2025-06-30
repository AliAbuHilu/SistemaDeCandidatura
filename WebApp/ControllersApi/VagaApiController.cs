using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels;

[ApiController]
[Route("api/vaga")]

public class VagaApiController : ControllerBase
{
    private readonly VagaService _service;

    public VagaApiController(VagaService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var vagas = _service.Listar();
        return Ok(vagas);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var vaga = _service.ObterPorId(id);
        if (vaga == null) return NotFound();
        return Ok(vaga);
    }

    [HttpPost]
    public IActionResult Post([FromBody] VagaViewModel vaga)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _service.Criar(vaga);
        return CreatedAtAction(nameof(Get), new { id = vaga.Id }, vaga);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] VagaViewModel vaga)
    {
        if (id != vaga.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _service.Atualizar(vaga);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.Excluir(id);
        return NoContent();
    }
}
