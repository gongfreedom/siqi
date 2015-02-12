using siqi.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Interfaces
{
    public interface ISeat
    {
        int Index { get; set; }

        IUserAgent User
        {
            get;
            set;
        }

        SeatInfo GetInfo();
       
    }
}
