using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iReception.Models.Dtos.FilterDtos
{
    public class FilterMinuteServiceDto
    {
        [Display(Name = "Id")]
        public int? MinId { get; set; }
        public int? MaxId { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        [Display(Name = "Price per minute")]
        public decimal? MinPricePerMinute { get; set; }
        public decimal? MaxPricePerMinute { get; set; }

        [Display(Name = "Rental time")]
        public int? MinTime { get; set; }
        public int? MaxTime { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }
        public bool IsNotAvailable { get; set; }

    }
}
