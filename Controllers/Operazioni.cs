using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // all endpoints require authentication
    public class Operazioni : ControllerBase
    {
        private readonly IOperazioniRepository _operazioniRepository;

        public Operazioni(IOperazioniRepository operazioniRepository)
        {
            _operazioniRepository = operazioniRepository;
        }

        // Helper: extract user ID from JWT claims
        private string GetUserId()
        {
            return User.FindFirstValue(JwtRegisteredClaimNames.NameId)
                ?? User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User ID not found in token.");
        }

        // Helper: check if the current user is Admin
        private bool IsAdmin()
        {
            return User.IsInRole("Admin");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperazioniDto>>> GetAllOperazioni([FromQuery] QueryElementOperazioni queryElement)
        {
            var operazioni = await _operazioniRepository.GetAllOperazioniAsync(queryElement, GetUserId(), IsAdmin());
            return Ok(operazioni);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperazioniDto>> GetOperazioneById(int id, [FromQuery] QueryElementOperazioniByPageSize queryElement)
        {
            var operazione = await _operazioniRepository.GetOperazioneByIdAsync(id, queryElement, GetUserId(), IsAdmin());
            if (operazione == null)
            {
                return NotFound();
            }
            return Ok(operazione);
        }

        [HttpPost]
        public async Task<ActionResult<OperazioniDto>> CreateOperazione(OperazioniRequestDto operazioneDto)
        {
            var createdOperazione = await _operazioniRepository.CreateOperazioneAsync(operazioneDto, GetUserId(), IsAdmin());
            if (createdOperazione == null)
            {
                return BadRequest("Cliente non trovato o non autorizzato.");
            }
            return CreatedAtAction(nameof(GetOperazioneById), new { id = createdOperazione.Id + 1 }, createdOperazione);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperazioniDto>> UpdateOperazione(int id, OperazioniRequestDto operazioneDto)
        {
            var updatedOperazione = await _operazioniRepository.UpdateOperazioneAsync(id, operazioneDto, GetUserId(), IsAdmin());
            if (updatedOperazione == null)
            {
                return BadRequest("Operazione non trovata o non autorizzato.");
            }
            return Ok(updatedOperazione);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperazione(int id)
        {
            var result = await _operazioniRepository.DeleteOperazioneAsync(id, GetUserId(), IsAdmin());
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}