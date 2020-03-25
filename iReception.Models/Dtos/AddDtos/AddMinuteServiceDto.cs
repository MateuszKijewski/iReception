using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iReception.Models.Dtos.AddDtos
{
    public class AddMinuteServiceDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price per minute")]
        public decimal PricePerMinute { get; set; }

        [Required]
        [Display(Name = "Minimal time")]
        public int MinTime { get; set; }

        [Required]
        [Display(Name = "Maximal time")]
        public int MaxTime { get; set; }

        [Required]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }
    }
}
