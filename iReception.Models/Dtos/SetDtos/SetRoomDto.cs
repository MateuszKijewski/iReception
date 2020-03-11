using iReception.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iReception.Models.Dtos.SetDtos
{
    public class SetRoomDto
    {
        public string Number { get; set; }
        public StandardEnum Standard { get; set; }
        public int Floor { get; set; }
        public decimal PricePerDay { get; set; }

        /* Relations */
        [Required]
        public int BuildingId { get; set; }

        /* Flags */
        public bool IsAvailable { get; set; }
        public bool IsDamaged { get; set; }
        public bool IsClean { get; set; }
        public bool IsRent { get; set; }
    }
}
