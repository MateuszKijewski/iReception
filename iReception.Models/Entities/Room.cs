using iReception.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public StandardEnum Standard { get; set; }
        public int Floor { get; set; }
        public decimal PricePerDay { get; set; }
        public string PhotoPath { get; set; }

        /* Relations */
        public int? ClientId { get; set; }
        public Client Client { get; set; }

        public int BuildingId { get; set; }
        public Building Building { get; set; }

        public IEnumerable<RoomToMinuteService> RoomToMinuteServices { get; set; }
        public IEnumerable<RoomToService> RoomToServices { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }

        /* Flags */
        public bool IsAvailable { get; set; }
        public bool IsDamaged { get; set; }
        public bool IsClean { get; set; }
        public bool IsRent { get; set; }
        public bool IsDeleted { get; set; }

    }
}
