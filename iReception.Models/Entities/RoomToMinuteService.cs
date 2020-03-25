using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class RoomToMinuteService
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int MinuteServiceId { get; set; }
        public MinuteService MinuteService { get; set; }

        public bool IsDeleted { get; set; }
    }
}
