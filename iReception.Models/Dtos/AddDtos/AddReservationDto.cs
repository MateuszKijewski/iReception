using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iReception.Models.Dtos.AddDtos
{
    public class AddReservationDto
    {
        [Required]
        [Display(Name = "Room")]
        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }

        //Will invoke MinuteServiceToReservation repo in service
        [Required]
        public Dictionary<int, int> MinuteServices { get; set; }

        [Required]
        [Display(Name = "Arrival date")]
        public DateTime ReservationStartDate { get; set; }

        [Required]
        [Display(Name = "Departure date")]
        public DateTime ReservationEndDate { get; set; }

        //Balance, paid, active and isdeleted will be set automatically in repo

    }
}
