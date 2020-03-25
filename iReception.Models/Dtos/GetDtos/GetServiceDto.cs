using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Dtos.GetDtos
{
    public class GetServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsAvailable { get; set; }
    }
}
