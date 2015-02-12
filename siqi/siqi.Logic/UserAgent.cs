using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Logic
{
    class UserAgent:Interfaces.IUserAgent
    {
        public UserAgent(Interfaces.Data.Model.User user,EC.ISession session)
        {
            Name = user.Name;
            ID = user.ID;
            Session = session;
        }
        public string Name
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }

        public EC.ISession Session
        {
            get;
            set;
        }

        public Interfaces.IRoom Room
        {
            get;
            set;
        }

        public Interfaces.IDesk Desk
        {
            get;
            set;
        }

        public Interfaces.ISeat Seat
        {
            get;
            set;
        }
    }
}
