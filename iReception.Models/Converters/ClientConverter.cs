using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Converters
{
    public class ClientConverter : IClientConverter
    {
        public Client AddClientDtoToClient(AddClientDto addClientDto)
        {
            return new Client
            {
                FirstName = addClientDto.FirstName,
                LastName = addClientDto.LastName,
                PhoneNumber = addClientDto.PhoneNumber,
                Email = addClientDto.Email,

                PaymentMethod = addClientDto.PaymentMethod,
                CreditCard = addClientDto.CreditCard,
                IdDocumentNumber = addClientDto.IdDocumentNumber
            };
        }

        public GetClientDto ClientToGetClientDto(Client client)
        {
            return new GetClientDto
            {
                Id = client.Id,

                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,

                PaymentMethod = client.PaymentMethod,
                CreditCard = client.CreditCard,
                IdDocumentNumber = client.IdDocumentNumber,

                Room = client.Room,

                IsDeleted = client.IsDeleted
            };
        }

        public Client SetClientDtoToClient(SetClientDto setClientDto)
        {
            return new Client
            {
                FirstName = setClientDto.FirstName,
                LastName = setClientDto.LastName,
                PhoneNumber = setClientDto.PhoneNumber,
                Email = setClientDto.Email,

                PaymentMethod = setClientDto.PaymentMethod,
                CreditCard = setClientDto.CreditCard,
                IdDocumentNumber = setClientDto.IdDocumentNumber
            };
        }
    }
}
