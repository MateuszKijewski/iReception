using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class Building
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string ShortName { get; set; }

        /* Relations */
        public IEnumerable<Room> Rooms { get; set; }

        /* Flags */
        public bool IsDeleted { get; set; }
        public bool IsAvailable { get; set; }
    }
}
