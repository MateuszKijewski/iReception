using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class RoomToService
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public bool IsDeleted { get; set; }
    }
}
