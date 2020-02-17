using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class Worker
    {
        public int Id { get; set; }

        /* Contact info */
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        /* Relations */
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
