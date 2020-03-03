using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iReception.Models.Dtos.FilterDtos
{
    public class FilterClientDto
    {
        [Display(Name = "Id")]
        public int? MinId { get; set; }
        public int? MaxId { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Display(Name = "Payment method")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Credit card number")]
        public string CreditCard { get; set; }

        [Display(Name = "Document number")]
        public string IdDocumentNumber { get; set; }
    }
}
