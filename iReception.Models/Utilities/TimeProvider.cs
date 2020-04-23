using System;
using System.Collections.Generic;
using System.Text;
using iReception.Repository.Interfaces;

namespace iReception.Repository
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
