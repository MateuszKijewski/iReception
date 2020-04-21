using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class MinuteServiceToReservation
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int MinuteServiceId { get; set; }
        public MinuteService MinuteService { get; set; }
        public int Duration { get; set; }
        public decimal Cost { get; set; } 
    }
}
