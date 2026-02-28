using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Clienti : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        private readonly IClientiRepository _clientiRepository;

        public Clienti(IClientiRepository clientiRepository, ApplicationDBContext context)
        {
            _clientiRepository = clientiRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientiDto>>> GetAllClienti([FromQuery] QueryElementClienti queryElement)
        {
            var clienti = await _clientiRepository.GetAllClientiAsync(queryElement);
            return Ok(clienti);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientiDto>> GetClienteById(int id)
        {
            var cliente = await _clientiRepository.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClientiDto>> CreateCliente(ClientiDto clienteDto)
        {
            var createdCliente = await _clientiRepository.CreateClienteAsync(clienteDto);
            return CreatedAtAction(nameof(GetClienteById), new { id = createdCliente }, createdCliente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClientiDto>> UpdateCliente(int id, ClientiDto clienteDto)
        {
            var updatedCliente = await _clientiRepository.UpdateClienteAsync(id, clienteDto);
            if (updatedCliente == null)
            {
                return NotFound();
            }
            return Ok(updatedCliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var result = await _clientiRepository.DeleteClienteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}