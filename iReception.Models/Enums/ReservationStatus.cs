using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Enums
{
    public enum ReservationStatus
    {
        New, // else
        Approaching, // <3 days to reservation
        Today, // reservation today
        Ongoing, // reservation ongoing
        Closing, // <2 days till the end of reservation
        Finished // finishing today
    }
}
