using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Logic.Dispatch.Handlers
{
    class SelectDeskHandler:MessageHandler<Protocol.SelectDesk>
    {

        protected override void OnExecute(Protocol.SelectDesk message, EC.ISession session, Interfaces.IUserAgent agent, Interfaces.ISiqiServer server)
        {
            Protocol.SelectDeskResponse response = new Protocol.SelectDeskResponse();
            response.Success = true;
            Interfaces.IRoom room = server.GetRoom(message.Room);
            if (room != null)
            {
                room.AddMessage(message, agent);
            }
            else
            {
                response.Success = false;
                response.Message = "所在房间不存在!";
            }
            server.Send(response,session);
        }
    }
}
