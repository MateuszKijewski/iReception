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

        /* Relations */
        public IEnumerable<RoomToService> RoomToServices { get; set; }

        /* Flags */
        public bool IsDeleted { get; set; }
        public bool IsAvailable { get; set; }
    }
}
