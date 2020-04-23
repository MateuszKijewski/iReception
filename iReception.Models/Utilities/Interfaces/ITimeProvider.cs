using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Repository.Interfaces
{
    public interface ITimeProvider
    {
        DateTime GetCurrentTime();
    }
}
