using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Dtos.AddDtos
{
    public class RoomServicesDto
    {
        public int RoomId { get; set; }
        public int[] AssignedIds { get; set; }
    }
}
