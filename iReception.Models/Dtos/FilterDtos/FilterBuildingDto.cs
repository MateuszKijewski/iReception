using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iReception.Models.Dtos.FilterDtos
{
    public class FilterBuildingDto
    {
        [Display(Name = "Id")]
        public int? MinId { get; set; }
        public int? MaxId { get; set; }

        public string Address { get; set; }
        
        [Display(Name = "Short name")]
        public string ShortName { get; set; }

        [Display(Name = "Availability")]
        public bool IsAvailable { get; set; }
        public bool IsNotAvailable { get; set; }
    }
}
