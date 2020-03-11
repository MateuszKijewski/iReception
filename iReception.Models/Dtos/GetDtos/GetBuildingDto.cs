using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iReception.Models.Dtos.GetDtos
{
    public class GetBuildingDto
    {
        public int Id { get; set; }
        
        public string Address { get; set; }

        [Display(Name = "Short name")]
        public string ShortName { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        /* Relations */
        public IEnumerable<Room> Rooms { get; set; }

        public override string ToString()
        {
            return ShortName;
        }
    }
}
