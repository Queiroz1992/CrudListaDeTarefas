using ListaDeTarefas.Application.Interfaces;
using ListaDeTarefas.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTarefasController : ControllerBase
    {
            private readonly ISubTarefaService _subTarefaService;

            public SubTarefasController(ISubTarefaService subTarefaService)
            {
                _subTarefaService = subTarefaService;
            }

            [HttpPost("{tarefaId}")]
            public async Task<ActionResult<SubTarefa>> CreateSubTarefa(int tarefaId, [FromBody] SubTarefa subTarefa)
            {
                var createdSubTarefa = await _subTarefaService.CreateSubTarefaAsync(tarefaId, subTarefa);
                return CreatedAtAction(nameof(GetSubTarePorId), new { id = createdSubTarefa.Id }, createdSubTarefa);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<SubTarefa>> GetSubTarePorId(int id)
            {
                var subTarefa = await _subTarefaService.GetSubTarefaPorIdAsync(id);
                if (subTarefa == null)
                {
                    return NotFound();
                }
                return subTarefa;
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateSubTarefa(int id, [FromBody] SubTarefa subTarefa)
            {
                if (id != subTarefa.Id)
                {
                    return BadRequest();
                }

                var updated = await _subTarefaService.UpdateSubTarefaAsync(subTarefa);
                if (!updated)
                {
                    return NotFound();
                }

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteSubTarefa(int id)
            {
                var deleted = await _subTarefaService.DeleteSubTarefaAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
        }
}
