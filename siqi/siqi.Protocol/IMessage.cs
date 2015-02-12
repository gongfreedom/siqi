using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Protocol
{
    public interface IMessage
    {
        string MsgID { get; set; }
    }

    public class MessageBase : IMessage
    {

        public string MsgID
        {
            get;
            set;
        }
    }
    
}
