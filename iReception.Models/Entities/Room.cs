using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Standard { get; set; }
        public int Floor { get; set; }
        public decimal FurnitureValue { get; set; }
        public decimal PricePerDay { get; set; }
        
        /* Flags */
        public bool IsDamaged { get; set; }
        public bool IsClean { get; set; }
        public bool IsRent { get; set; }
    }
}
