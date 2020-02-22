using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        /* Flags */
        public bool IsDamaged { get; set; }
    }
}
