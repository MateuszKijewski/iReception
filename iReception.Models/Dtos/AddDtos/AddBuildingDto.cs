using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iReception.Models.Dtos.AddDtos
{
    public class AddBuildingDto
    {
        [Required]        
        public string Address { get; set; }

        [Required]
        [Display(Name = "Short name")]
        public string ShortName { get; set; }
    }
}
