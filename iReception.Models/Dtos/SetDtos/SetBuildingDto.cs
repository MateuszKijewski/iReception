using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iReception.Models.Dtos.SetDtos
{
    public class SetBuildingDto
    {
        public string Address { get; set; }

        [Display(Name = "Short name")]
        public string ShortName { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }
    }
}
