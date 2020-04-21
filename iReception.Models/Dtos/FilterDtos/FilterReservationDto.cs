using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using iReception.Models.Enums;

namespace iReception.Models.Dtos.FilterDtos
{
    public class FilterReservationDto
    {
        [Display(Name = "Id")]
        public int? MinId { get; set; }
        public int? MaxId { get; set; }

        [Display(Name = "Balance")]
        public decimal MinBalance { get; set; }
        public decimal MaxBalance { get; set; }

        [Display(Name = "Reservations from")]
        public DateTime MinReservationDate { get; set; }
        
        [Display(Name = "Room")]
        public string Room { get; set; }

        [Display(Name = "Reservations to")]
        public DateTime MaxReservationDate { get; set; }

        [Display(Name = "Reservation status")]
        public ReservationStatus ReservationStatus { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public bool IsNotActive { get; set; }
    }
}
