using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Dtos.GetDtos
{
    public class GetInvoicePositionDto
    {
        public string Name { get; set; }
        public decimal PriceNetto { get; set; }
        public decimal PriceBrutto { get; set; }
        public int Amount { get; set; }
        public int VatRate { get; set; }
        public decimal VatValue { get; set; }
    }
}
