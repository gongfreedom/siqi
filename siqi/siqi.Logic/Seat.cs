using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Logic
{
    public class Seat:Interfaces.ISeat
    {
        public int Index
        {
            get;
            set;
        }

        public Interfaces.IUserAgent User
        {
            get;
            set;
        }

        public Protocol.SeatInfo GetInfo()
        {
            return new Protocol.SeatInfo { Index=Index, UserID= User.ID, UserName= User.Name };
        }
    }
}
