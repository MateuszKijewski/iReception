﻿using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
