using siqi.Interfaces;
using siqi.Logic.Dispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Logic
{
    class Desk : Interfaces.IDesk
    {
        public Desk(string id, string name,int seats)
        {
            ID = id;
            Name = name;
            Seats = new List<Interfaces.ISeat>();
            Visitors = new List<IUserAgent>();
            for (int i = 0; i < seats; i++)
            {
                Seats.Add(new Seat { Index=i+1 });
            }
        }

        private UserSegment mUserSegment = new UserSegment(100);

        private Queue<MessageToken> mQueue = new Queue<MessageToken>();

        public string ID
        {
            get;
            set;
        }
        public IList<IUserAgent> Visitors { get; private set; }
    
    
        public string Name
        {
            get;
            set;
        }

        public IList<Interfaces.ISeat> Seats
        {
            get;
            private set;
        }

        public void Add(Interfaces.ISeat seat)
        {
            Seats.Add(seat);
        }

        public Interfaces.IRoom Room
        {
            get;
            set;
        }

        public string In(Interfaces.IUserAgent user, int seatIndex)
        {
            Protocol.Result result = new Protocol.Result();
            result.Success = true;

            if (user.Desk != null)
                user.Desk.Exit(user);
            for (int i = 0; i < Seats.Count; i++)
            {
                Interfaces.ISeat seat = Seats[i];
                if (seat.Index == seatIndex)
                {
                    lock (seat)
                    {
                        if (seat.User == null || seat.User == user)
                        {
                            seat.User = user;
                            user.Desk = this;


                        }
                        else
                        {
                            result.Success = false;
                            return "位置已经被占用!";
                        }
                    }
                }
            }
            lock (Visitors)
            {
                if (!Visitors.Contains(user))
                    Visitors.Add(user);
                System.Threading.Interlocked.Increment(ref mVersion);
            }
            return null;

        }

        public void Exit(Interfaces.IUserAgent user)
        {
            if (user.Desk == this)
            {
               
                if(user.Seat !=null)
                    user.Seat.User = null;
                user.Seat = null;
                user.Desk = null;
                lock (Visitors)
                {
                    Visitors.Remove(user);
                    System.Threading.Interlocked.Increment(ref mVersion);
                }

            }
        }

        public void AddMessage(Protocol.IMessage message, Interfaces.IUserAgent user)
        {
            lock (this)
            {
                mQueue.Enqueue(new MessageToken { Message = message, UserAgent = user });
            }
        }

        public void Execute(ISiqiServer server)
        {
            MessageToken token = null;
            lock (this)
            {
                if (mQueue.Count > 0)
                    token = mQueue.Dequeue();
            }
            if (token != null)
                OnExecute(token, server);
        }   

        public ArraySegment<IUserAgent> GetAgents()
        {
            if (mUserSegment.Version != Version)
            {
                lock (Visitors)
                {
                    mUserSegment.Reset();
                    foreach (IUserAgent agent in Visitors)
                    {
                        mUserSegment.Enqueue(agent);
                    }
                    for (int i = 0; i < Seats.Count; i++)
                    {
                        Interfaces.ISeat seat = Seats[i];
                        if (seat.User != null)
                            mUserSegment.Enqueue(seat.User);
                    }
                    mUserSegment.Version = Version;
                }
            }
            return mUserSegment.GetAgents();
        }

        private long mVersion = 0;

        public long Version
        {
            get
            {
                return mVersion;
            }
        }

        public Protocol.DeskInfo GetInfo()
        {
            Protocol.DeskInfo result = new Protocol.DeskInfo();
            result.Seats = new List<Protocol.SeatInfo>();
            result.DeskID = this.ID;
            result.Name = this.Name;
            for (int i = 0; i < Seats.Count; i++)
            {
                Interfaces.ISeat seat = Seats[i];
                result.Seats.Add(seat.GetInfo());
            }
            return result;
        }

        protected virtual void OnExecute(MessageToken token, ISiqiServer server)
        {
            //添加处理逻辑
        }

    }
}
