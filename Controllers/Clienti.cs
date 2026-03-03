using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // all endpoints require authentication
    public class Clienti : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        private readonly IClientiRepository _clientiRepository;

        public Clienti(IClientiRepository clientiRepository, ApplicationDBContext context)
        {
            _clientiRepository = clientiRepository;
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(JwtRegisteredClaimNames.NameId)
                ?? User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User ID not found in token.");
        }

        private bool IsAdmin()
        {
            return User.IsInRole("Admin");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientiDto>>> GetAllClienti([FromQuery] QueryElementClienti queryElement)
        {
            var clienti = await _clientiRepository.GetAllClientiAsync(queryElement, GetUserId(), IsAdmin());
            return Ok(clienti);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientiDto>> GetClienteById(int id)
        {
            var cliente = await _clientiRepository.GetClienteByIdAsync(id, GetUserId(), IsAdmin());
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClientiDto>> CreateCliente(ClientiDto clienteDto)
        {
            var createdCliente = await _clientiRepository.CreateClienteAsync(clienteDto, GetUserId());
            return CreatedAtAction(nameof(GetClienteById), new { id = createdCliente }, createdCliente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClientiDto>> UpdateCliente(int id, ClientiDto clienteDto)
        {
            var updatedCliente = await _clientiRepository.UpdateClienteAsync(id, clienteDto, GetUserId(), IsAdmin());
            if (updatedCliente == null)
            {
                return NotFound();
            }
            return Ok(updatedCliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var result = await _clientiRepository.DeleteClienteAsync(id, GetUserId(), IsAdmin());
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}