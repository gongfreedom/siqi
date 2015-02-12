using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using siqi.Interfaces;
namespace siqi.Logic.Dispatch.Handlers
{
    public class SelectRoomHandler : MessageHandler<Protocol.SelectRoom>
    {


        protected override void OnExecute(Protocol.SelectRoom message, EC.ISession session, Interfaces.IUserAgent agent, Interfaces.ISiqiServer server)
        {
            Protocol.SelectRoomResponse response = new Protocol.SelectRoomResponse();
            response.MsgID = message.MsgID;
            response.Success = false;
            IRoom room = server.GetRoom(message.Room);
            if (room != null)
            {
                response.Success = true;
                response.Room = room.ID;
            }
            else
            {
                response.Message = "room not found!";
            }
            server.Send(response, session);
        }
    }
}
