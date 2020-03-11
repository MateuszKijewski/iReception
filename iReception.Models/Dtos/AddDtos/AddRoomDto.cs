﻿using iReception.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iReception.Models.Dtos.AddDtos
{
    public class AddRoomDto
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public StandardEnum Standard { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        [Display(Name = "Price per day")]
        public decimal PricePerDay { get; set; }

        [Required]
        public int BuildingId { get; set; }
    }
}