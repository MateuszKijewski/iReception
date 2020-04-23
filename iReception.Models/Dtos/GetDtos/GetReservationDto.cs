using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using iReception.Models.Entities;
using iReception.Models.Enums;

namespace iReception.Models.Dtos.GetDtos
{
    public class GetReservationDto
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        [Display(Name = "Arrival date")]
        public DateTime ReservationStartDate { get; set; }

        [Display(Name = "Departure date")]
        public DateTime ReservationEndDate { get; set; }

        public ReservationStatus Status { get; set; }
        public bool Paid { get; set; }

        /* Relations */
        public GetRoomDto Room { get; set; }
        public GetClientDto Client { get; set; }

        public IEnumerable<MinuteServiceToReservation> MinuteServicesToReservation { get; set; }
    }
}
