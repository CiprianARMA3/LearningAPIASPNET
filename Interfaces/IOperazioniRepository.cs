using WebAPI.DTOs;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface IOperazioniRepository
    {
        Task<IEnumerable<OperazioniDto>> GetAllOperazioniAsync(QueryElementOperazioni queryElement, string userId, bool isAdmin);
        Task<OperazioniDto?> GetOperazioneByIdAsync(int id, QueryElementOperazioniByPageSize queryElement, string userId, bool isAdmin); 
        Task<OperazioniDto?> CreateOperazioneAsync(OperazioniRequestDto operazioneDto, string userId, bool isAdmin);
        Task<OperazioniDto?> UpdateOperazioneAsync(int id, OperazioniRequestDto operazioneDto, string userId, bool isAdmin);
        Task<bool> DeleteOperazioneAsync(int id, string userId, bool isAdmin);
    }
}