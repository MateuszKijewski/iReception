using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Dtos.SetDtos
{
    public class SetMinuteServiceDto
    {        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerMinute { get; set; }
        public int MinTime { get; set; }
        public int MaxTime { get; set; }
        

        /* Flags */     
        public bool IsAvailable { get; set; }
    }
}
