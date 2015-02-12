using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Logic.Dispatch.Handlers
{
    public class ListRoomHandler:MessageHandler<Protocol.ListRoom>
    {
        protected override void OnExecute(Protocol.ListRoom message, EC.ISession session, Interfaces.IUserAgent agent, Interfaces.ISiqiServer server)
        {
            Protocol.ListRoomResponse response = server.ListRoom();
            response.MsgID = message.MsgID;
            server.Send(response, session);
        }
    }
}
