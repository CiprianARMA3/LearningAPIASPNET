using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Operazioni : ControllerBase
    {
        private readonly IOperazioniRepository _clientiRepository;

        public Operazioni(IOperazioniRepository clientiRepository)
        {
            _clientiRepository = clientiRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperazioniDto>>> GetAllOperazioni([FromQuery] QueryElementOperazioni queryElement)
        {
            var operazioni = await _clientiRepository.GetAllOperazioniAsync(queryElement);
            return Ok(operazioni);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperazioniDto>> GetOperazioneById(int id , [FromQuery] QueryElementOperazioniByPageSize queryElement)
        {
            var operazione = await _clientiRepository.GetOperazioneByIdAsync(id, queryElement);
            if (operazione == null)
            {
                return NotFound();
            }
            return Ok(operazione);
        }

        [HttpPost]
        public async Task<ActionResult<OperazioniDto>> CreateOperazione(OperazioniRequestDto operazioneDto)
        {
            var createdOperazione = await _clientiRepository.CreateOperazioneAsync(operazioneDto);
            if (createdOperazione == null)
            {
                return BadRequest("Cliente non trovato.");
            }
            return CreatedAtAction(nameof(GetOperazioneById), new { id = createdOperazione.Id+1 }, createdOperazione);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperazioniDto>> UpdateOperazione(int id, OperazioniRequestDto operazioneDto)
        {
            var updatedOperazione = await _clientiRepository.UpdateOperazioneAsync(id, operazioneDto);
            if (updatedOperazione == null)
            {
                return BadRequest("Operazione non trovata o Cliente non valido.");
            }
            return Ok(updatedOperazione);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperazione(int id)
        {
            var result = await _clientiRepository.DeleteOperazioneAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}