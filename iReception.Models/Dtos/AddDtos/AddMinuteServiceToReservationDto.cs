using System;
using System.Collections.Generic;
using System.Text;
using iReception.Models.Entities;

namespace iReception.Models.Dtos.AddDtos
{
    public class AddMinuteServiceToReservationDto
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int MinuteServiceId { get; set; }
        public MinuteService MinuteService { get; set; }
        public int Duration { get; set; }
    }
}
