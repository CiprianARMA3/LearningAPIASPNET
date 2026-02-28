using WebAPI.DTOs;
using WebAPI.Models;
namespace WebAPI.Mappers
{
    public static class ClientiMapper
    {
        public static ClientiDto ToDto(this  Clienti cliente)
        {
            return new ClientiDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cognome = cliente.Cognome,
                Email = cliente.Email,
                Telefono = cliente.Telefono
            };
        }

        public static  Clienti ToEntity(this ClientiDto clienteDto)
        {
            return new  Clienti
            {
                Nome = clienteDto.Nome,
                Cognome = clienteDto.Cognome,
                Email = clienteDto.Email,
                Telefono = clienteDto.Telefono ?? string.Empty
            };
        }
        public static Clienti UpdateModel(this  Clienti cliente, ClientiDto clienteDto)
        {
            cliente.Nome = clienteDto.Nome;
            cliente.Cognome = clienteDto.Cognome;
            cliente.Email = clienteDto.Email;
            return cliente;
        }
    }
}