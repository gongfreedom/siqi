using Beetle.Express;
using EC;
using siqi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Logic.Dispatch.Handlers
{
    public class TalkHandler:MessageHandler<Protocol.Talk>
    {
        protected override void OnExecute(Protocol.Talk message, ISession session, IUserAgent agent, ISiqiServer server)
        {
            if (message.To != null)
            {
                IChannel channel = server.Application.Server.GetChannel(message.To);
                server.Send(message, channel);
            }
            else
            {
                if (agent.Desk != null)
                {
                    server.Send(message, agent.Desk.GetAgents());
                }
                else if (agent.Room != null)
                {
                    server.Send(message, agent.Room.GetAgents());
                }
                else
                {
                   
                }
            }
        }
    }
}
