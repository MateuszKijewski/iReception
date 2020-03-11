using iReception.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iReception.Models.Dtos.FilterDtos
{
    public class FilterRoomDto
    {
        [Display(Name = "Id")]
        public int? MinId { get; set; }
        public int? MaxId { get; set; }

        public string Number { get; set; }
        public StandardEnum Standard { get; set; }

        [Display(Name = "Floor")]
        public int? MinFloor { get; set; }
        public int? MaxFloor { get; set; }

        [Display(Name = "Price per day")]
        public decimal? MinPricePerDay { get; set; }
        public decimal? MaxPricePerDay { get; set; }

        public string Client { get; set; }
        public int? BuildingId { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }
        public bool IsNotAvailable { get; set; }

        [Display(Name = "Damaged")]
        public bool IsDamaged { get; set; }
        public bool IsNotDamaged { get; set; }

        [Display(Name = "Clean")]
        public bool IsClean { get; set; }
        public bool IsNotClean { get; set; }

        [Display(Name = "Rent")]
        public bool IsRent { get; set; }
        public bool IsNotRent { get; set; }
    }
}
