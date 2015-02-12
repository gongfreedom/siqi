using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EC;
namespace siqi.Logic.Dispatch
{
    public class Dispatcher
    {
        public Dispatcher()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(Run);
        }
        public Interfaces.ISiqiServer Server
        {
            get;
            set;
        }

        private Queue<MessageToken> mQueue = new Queue<MessageToken>();

        private Dictionary<Type, IMessageHandler> mHandlers = new Dictionary<Type, IMessageHandler>();

        public void Register<MSG, HANDLER>() where HANDLER : IMessageHandler, new()
        {
            mHandlers[typeof(MSG)] = new HANDLER();
        }

        public void Add(MessageToken item)
        {
            lock (this)
            {
                mQueue.Enqueue(item);
            }
        }

        public MessageToken GetItem()
        {
            lock (this)
            {
                if (mQueue.Count > 0)
                    return mQueue.Dequeue();
                return null;
            }
        

        }

        public void Run(object state)
        {

            while (true)
            {
                IMessageHandler handler = null;
                try
                {
                    MessageToken job = GetItem();

                    if (job != null)
                    {
                        
                        if (mHandlers.TryGetValue(job.Message.GetType(), out handler))
                        {
                            handler.Execute(job);
                        }
                        else
                        {
                            "{0} message handler not found".Log4Error(job.Message.GetType());
                        }
                    }
                    else
                        System.Threading.Thread.Sleep(10);
                }
                catch (Exception e_)
                {
                    if (handler != null)
                        "{0} invoke error {1}".Log4Error(e_, handler.GetType(), e_.Message);
                    else
                        "Dispatcher error {0}".Log4Error(e_, e_.Message);
                }
            }
        }
    }
}
