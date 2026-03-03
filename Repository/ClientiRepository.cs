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

        public async Task<ClientiDto> CreateClienteAsync(ClientiDto clienteDto, string userId)
        {
            var entity = clienteDto.ToEntity();
            entity.AppUserId = userId; // assign the current user as the owner
            _context.Clienti.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToDto();
        }

        public async Task<bool> DeleteClienteAsync(int id, string userId, bool isAdmin)
        {
            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null)
            {
                return false;
            }
            // RLS: only the owner or an admin can delete
            if (!isAdmin && cliente.AppUserId != userId)
            {
                return false;
            }
            _context.Clienti.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ClientiDto>> GetAllClientiAsync(QueryElementClienti queryElement, string userId, bool isAdmin)
        {
            var query = _context.Clienti.AsQueryable();

            // RLS: non-admin users only see their own clienti
            if (!isAdmin)
            {
                query = query.Where(c => c.AppUserId == userId);
            }

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

        public async Task<ClientiDto?> GetClienteByIdAsync(int id, string userId, bool isAdmin)
        {
            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null)
            {
                return null;
            }
            // RLS: non-admin users can only see their own clienti
            if (!isAdmin && cliente.AppUserId != userId)
            {
                return null;
            }
            return cliente.ToDto();
        }

        public async Task<ClientiDto?> UpdateClienteAsync(int id, ClientiDto clienteDto, string userId, bool isAdmin)
        {
            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null)
            {
                return null;
            }
            // RLS: only the owner or an admin can update
            if (!isAdmin && cliente.AppUserId != userId)
            {
                return null;
            }
            _context.Entry(cliente).CurrentValues.SetValues(clienteDto.ToEntity());
            // preserve the original owner
            cliente.AppUserId = cliente.AppUserId;
            await _context.SaveChangesAsync();
            return cliente.ToDto();
        }
    }
}