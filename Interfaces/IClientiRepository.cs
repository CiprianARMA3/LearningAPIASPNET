using WebAPI.DTOs;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface IClientiRepository
    {
        Task<IEnumerable<ClientiDto>> GetAllClientiAsync(QueryElementClienti queryElement, string userId, bool isAdmin);
        Task<ClientiDto?> GetClienteByIdAsync(int id, string userId, bool isAdmin); 
        Task<ClientiDto> CreateClienteAsync(ClientiDto clienteDto, string userId);
        Task<ClientiDto?> UpdateClienteAsync(int id, ClientiDto clienteDto, string userId, bool isAdmin);
        Task<bool> DeleteClienteAsync(int id, string userId, bool isAdmin);
    }
}