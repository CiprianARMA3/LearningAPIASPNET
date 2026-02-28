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

        public async Task<OperazioniDto?> CreateOperazioneAsync(OperazioniRequestDto operazioneDto)
        {
            var cliente = await _context.Clienti.FindAsync(operazioneDto.IdCliente);
            if (cliente == null)
            {
                return null;
            }

            var entity = operazioneDto.ToEntity();
            _context.Operazioni.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToDto();
        }

        public async Task<bool> DeleteOperazioneAsync(int id )
        {
            var operazione = await _context.Operazioni.FindAsync(id);
            if (operazione == null)
            {
                return false;
            }
            _context.Operazioni.Remove(operazione);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<OperazioniDto>> GetAllOperazioniAsync(QueryElementOperazioni queryElement)
        {
            var query = _context.Operazioni.AsQueryable();
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

            var operazioni = await query.Include(o => o.Cliente).ToListAsync();
            return operazioni.Select(o => o.ToDto());
        }

        public async Task<OperazioniDto?> GetOperazioneByIdAsync(int id , QueryElementOperazioniByPageSize queryElement)
        {
            var query = _context.Operazioni.Where(o => o.Id == id).Include(o => o.Cliente).AsQueryable();
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

        public async Task<OperazioniDto?> UpdateOperazioneAsync(int id, OperazioniRequestDto operazioneDto)
        {
            var operazione = await _context.Operazioni.FindAsync(id);
            if (operazione == null)
            {
                return null;
            }

            var cliente = await _context.Clienti.FindAsync(operazioneDto.IdCliente);
            if (cliente == null)
            {
                return null;
            }

            _context.Entry(operazione).CurrentValues.SetValues(operazioneDto.ToEntity());
            await _context.SaveChangesAsync();
            return operazione.ToDto();
        }
        }
    }
