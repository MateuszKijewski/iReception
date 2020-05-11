using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Dtos.GetDtos
{
    public class GetInvoiceDto
    {
        public DateTime IssueDate { get; set; }
        public DateTime PaymentDate { get; set; }

        public List<GetInvoicePositionDto> Positions { get; set; }
        public GetHotelCompanyDto HotelCompanyDetails { get; set; }
        public GetClientDto ClientDetails { get; set; }
    }
}
