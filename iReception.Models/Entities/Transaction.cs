using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }

        /* Relations */
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}
