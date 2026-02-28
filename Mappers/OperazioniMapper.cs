using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Mappers
{
    public static class OperazioniMapper
    {
        public static OperazioniDto ToDto(this Operazioni operazione)
        {
            return new OperazioniDto
            {
                Id = operazione.Id,
                IdCliente = operazione.IdCliente,
                Cliente = operazione.Cliente,
                Descrizione = operazione.Descrizione,
                Importo = operazione.Importo,
                Data = operazione.Data
            };
        }

        public static Operazioni ToEntity(this OperazioniRequestDto operazioneDto)
        {
            return new Operazioni
            {
                IdCliente = operazioneDto.IdCliente,
                Descrizione = operazioneDto.Descrizione,
                Importo = operazioneDto.Importo,
                Data = operazioneDto.Data
            };
        }
        public static Operazioni UpdateEntity(this Operazioni operazione, OperazioniRequestDto operazioneDto)
        {
            operazione.Descrizione = operazioneDto.Descrizione;
            operazione.Importo = operazioneDto.Importo;
            operazione.Data = operazioneDto.Data;
            return operazione;
        }
    }
}