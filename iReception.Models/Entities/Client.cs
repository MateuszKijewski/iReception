using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class Client
    {
        /* Contact info */
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        /* Billing/Identification info */
        public string PaymentMethod { get; set; }
        public string CreditCard { get; set; }
        public string IdDocumentNumber { get; set; }
    }
}
