using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Converters.Interfaces
{
    public interface IClientConverter
    {
        Client AddClientDtoToClient(AddClientDto addClientDto);
        GetClientDto ClientToGetClientDto(Client client);
        Client SetClientDtoToClient(SetClientDto setClientDto);
    }
}
