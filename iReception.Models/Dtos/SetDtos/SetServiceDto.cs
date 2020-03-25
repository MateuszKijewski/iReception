using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Dtos.SetDtos
{
    public class SetServiceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsAvailable { get; set; }
    }
}
