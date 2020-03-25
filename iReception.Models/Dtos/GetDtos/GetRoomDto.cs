using iReception.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Dtos.GetDtos
{
    public class GetRoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public StandardEnum Standard { get; set; }
        public int Floor { get; set; }
        public decimal PricePerDay { get; set; }
        public string PhotoPath { get; set; }

        /* Relations */
        public GetBuildingDto Building { get; set; }


        /* Flags */
        public bool IsAvailable { get; set; }
        public bool IsDamaged { get; set; }
        public bool IsClean { get; set; }
        public bool IsRent { get; set; }
    }
}
