using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.Mappers;

namespace WebAPI.Repository
{

    public class ClientiRepository : IClientiRepository
    {
        private readonly ApplicationDBContext _context;
        public ClientiRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ClientiDto> CreateClienteAsync(ClientiDto clienteDto)
        {
            _context.Clienti.Add(clienteDto.ToEntity());
            _context.SaveChanges();
            return clienteDto;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null)
            {
                return false;
            }
            _context.Clienti.Remove(cliente);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<ClientiDto>> GetAllClientiAsync(QueryElementClienti queryElement)
        {
            //var clienti = _context.Clienti.Select(c => c.ToDto()).ToList();

            var query = _context.Clienti.AsQueryable();
            if (!string.IsNullOrEmpty(queryElement.Nome))
            {
                query = query.Where(c => c.Nome.Contains(queryElement.Nome));
            }
            if (!string.IsNullOrEmpty(queryElement.Cognome))
            {
                query = query.Where(c => c.Cognome.Contains(queryElement.Cognome));
            }
            if (!string.IsNullOrEmpty(queryElement.Email))
            {
                query = query.Where(c => c.Email.Contains(queryElement.Email));
            }
            if (queryElement.PageNumber > 0 && queryElement.PageSize > 0)
            {
                query = query.Skip((queryElement.PageNumber - 1) * queryElement.PageSize).Take(queryElement.PageSize);
            }
            var clienti = await query.Select(c => c.ToDto()).ToListAsync<ClientiDto>();
            return clienti;
        }

        public async Task<ClientiDto?> GetClienteByIdAsync(int id)
        {
            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null)
            {
                return null;
            }
            return cliente.ToDto();
        }

        public async Task<ClientiDto?> UpdateClienteAsync(int id, ClientiDto clienteDto)
        {
            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null)
            {
                return null;
            }
            _context.Entry(cliente).CurrentValues.SetValues(clienteDto.ToEntity());
            _context.SaveChanges();
            return cliente.ToDto();
        }
    }
}