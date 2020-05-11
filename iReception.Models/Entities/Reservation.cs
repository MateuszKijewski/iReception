using System;
using System.Collections.Generic;
using System.Text;
using iReception.Models.Enums;

namespace iReception.Models.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPaid { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }

        /* Relations */
        public int RoomId { get; set; }
        public Room Room { get; set; }
        
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public IEnumerable<MinuteServiceToReservation> MinuteServicesReserved { get; set; }

    }
}
