using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Logic.Dispatch
{
    public interface IMessageHandler
    {
        void Execute(MessageToken e);
    }

    public abstract class MessageHandler<T>:IMessageHandler
    {
        public void Execute(MessageToken e)
        {

            OnExecute((T)e.Message, e.Session,e.UserAgent, e.Server);
        }
        protected abstract void OnExecute(T message, EC.ISession session,Interfaces.IUserAgent agent, Interfaces.ISiqiServer server);
    }
}
