using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.Mappers;

namespace WebAPI.Repository
{

    public class OperazioniRepository : IOperazioniRepository
    {
        private readonly ApplicationDBContext _context;
        public OperazioniRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<OperazioniDto?> CreateOperazioneAsync(OperazioniRequestDto operazioneDto, string userId, bool isAdmin)
        {
            // Verify the cliente exists
            var cliente = await _context.Clienti.FindAsync(operazioneDto.IdCliente);
            if (cliente == null)
            {
                return null;
            }
            // RLS: non-admin users can only create operazioni for their own clienti
            if (!isAdmin && cliente.AppUserId != userId)
            {
                return null;
            }

            var entity = operazioneDto.ToEntity();
            _context.Operazioni.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToDto();
        }

        public async Task<bool> DeleteOperazioneAsync(int id, string userId, bool isAdmin)
        {
            var operazione = await _context.Operazioni
                .Include(o => o.Cliente)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (operazione == null)
            {
                return false;
            }
            // RLS: non-admin users can only delete their own operazioni
            if (!isAdmin && operazione.Cliente?.AppUserId != userId)
            {
                return false;
            }
            _context.Operazioni.Remove(operazione);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OperazioniDto>> GetAllOperazioniAsync(QueryElementOperazioni queryElement, string userId, bool isAdmin)
        {
            var query = _context.Operazioni.Include(o => o.Cliente).AsQueryable();

            // RLS: non-admin users only see operazioni belonging to their clienti
            if (!isAdmin)
            {
                query = query.Where(o => o.Cliente != null && o.Cliente.AppUserId == userId);
            }

            if (queryElement.IdCliente > 0)
            {
                query = query.Where(o => o.IdCliente == queryElement.IdCliente);
            }
            if (!string.IsNullOrEmpty(queryElement.Descrizione))
            {
                query = query.Where(o => o.Descrizione != null && o.Descrizione.Contains(queryElement.Descrizione));
            }
            if (queryElement.PageNumber > 0 && queryElement.PageSize > 0)
            {
                query = query.Skip((queryElement.PageNumber - 1) * queryElement.PageSize).Take(queryElement.PageSize);
            }

            var operazioni = await query.ToListAsync();
            return operazioni.Select(o => o.ToDto());
        }

        public async Task<OperazioniDto?> GetOperazioneByIdAsync(int id, QueryElementOperazioniByPageSize queryElement, string userId, bool isAdmin)
        {
            var query = _context.Operazioni
                .Where(o => o.Id == id)
                .Include(o => o.Cliente)
                .AsQueryable();

            // RLS: non-admin users can only see their own operazioni
            if (!isAdmin)
            {
                query = query.Where(o => o.Cliente != null && o.Cliente.AppUserId == userId);
            }

            if (queryElement.PageNumber > 0 && queryElement.PageSize > 0)
            {
                query = query.Skip((queryElement.PageNumber - 1) * queryElement.PageSize).Take(queryElement.PageSize);
            }
            var operazione = await query.FirstOrDefaultAsync();
            if (operazione == null)
            {
                return null;
            }
            return operazione.ToDto();
        }

        public async Task<OperazioniDto?> UpdateOperazioneAsync(int id, OperazioniRequestDto operazioneDto, string userId, bool isAdmin)
        {
            var operazione = await _context.Operazioni
                .Include(o => o.Cliente)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (operazione == null)
            {
                return null;
            }
            // RLS: non-admin users can only update their own operazioni
            if (!isAdmin && operazione.Cliente?.AppUserId != userId)
            {
                return null;
            }

            var cliente = await _context.Clienti.FindAsync(operazioneDto.IdCliente);
            if (cliente == null)
            {
                return null;
            }
            // RLS: non-admin users can't reassign operazioni to someone else's cliente
            if (!isAdmin && cliente.AppUserId != userId)
            {
                return null;
            }

            _context.Entry(operazione).CurrentValues.SetValues(operazioneDto.ToEntity());
            await _context.SaveChangesAsync();
            return operazione.ToDto();
        }
    }
}
