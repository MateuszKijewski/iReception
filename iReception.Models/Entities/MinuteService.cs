using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class MinuteService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerMinute { get; set; }
        public int MinTime { get; set; }
        public int MaxTime { get; set; }

        /* Relations */
        public IEnumerable<RoomToMinuteService> RoomToMinuteServices { get; set; }
    }
}
