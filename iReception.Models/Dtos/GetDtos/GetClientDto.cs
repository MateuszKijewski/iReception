using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iReception.Models.Dtos.GetDtos
{
    public class GetClientDto
    {
        public int Id { get; set; }

        /* Contact info */
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email address")]
        public string Email { get; set; }

        /* Billing/Identification info */
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }
        [Display(Name = "Credit card number")]
        public string CreditCard { get; set; }
        [Display(Name = "Document number")]
        public string IdDocumentNumber { get; set; }

        /* Related objects */
        public Room? Room { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }

        /* Flags */
        public bool IsDeleted { get; set; }
    }
}
