using WebAPI.DTOs;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface IClientiRepository
    {
        Task<IEnumerable<ClientiDto>> GetAllClientiAsync(QueryElementClienti queryElement);
        Task<ClientiDto?> GetClienteByIdAsync(int id); 
        Task<ClientiDto> CreateClienteAsync(ClientiDto clienteDto);
        Task<ClientiDto?> UpdateClienteAsync(int id, ClientiDto clienteDto);
        Task<bool> DeleteClienteAsync(int id);
    }
}