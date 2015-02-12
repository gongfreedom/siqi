using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Logic.Dispatch
{
    public class MessageToken
    {
        public Protocol.IMessage Message
        {
            get;
            set;
        }

        public Interfaces.ISiqiServer Server
        {
            get;
            set;
        }

        public Interfaces.IUserAgent UserAgent
        {
            get;
            set;
        }

        public EC.ISession Session
        {
            get;
            set;
        }
    }
}
