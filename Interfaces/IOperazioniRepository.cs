using WebAPI.DTOs;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface IOperazioniRepository
    {
        Task<IEnumerable<OperazioniDto>> GetAllOperazioniAsync(QueryElementOperazioni queryElement);
        Task<OperazioniDto?> GetOperazioneByIdAsync(int id , QueryElementOperazioniByPageSize queryElement); 
        Task<OperazioniDto?> CreateOperazioneAsync(OperazioniRequestDto operazioneDto);
        Task<OperazioniDto?> UpdateOperazioneAsync(int id, OperazioniRequestDto operazioneDto);
        Task <bool> DeleteOperazioneAsync(int id);
    }
}