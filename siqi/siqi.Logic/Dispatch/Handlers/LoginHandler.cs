using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using siqi.Interfaces;

namespace siqi.Logic.Dispatch.Handlers
{
    public class LoginHandler :MessageHandler<Protocol.Login>
    {
        protected override void OnExecute(Protocol.Login message, EC.ISession session, Interfaces.IUserAgent agent, Interfaces.ISiqiServer server)
        {
            Protocol.LoginResponse response = new Protocol.LoginResponse();
            response.MsgID = message.MsgID;
            response.Success = false;
            try
            {
                Interfaces.Data.Model.User user = server.LoginHandler.Login(message.EMail, message.Password);
                if (user != null)
                {
                    agent = new UserAgent(user, session);
                    session[SESSION_KEY.USER] = agent;
                    server.AddUser(agent);
                    response.Success = true;
                }
                else
                {
                    response.Message = "用户名和密码不正确!";
                }
            }
            catch (Exception e_)
            {
                response.Message = e_.Message;
            }
            server.Send(response, session);
        }
        
    }
   
}
