using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using siqi.Protocol;
using siqi.Interfaces;
using siqi.Logic.Dispatch;
namespace siqi.Logic
{
    class Room:Interfaces.IRoom
    {
        public Room(string id, string name)
        {
            ID = id;
            Name = name;
            Desks = new List<IDesk>();
            Users = new Dictionary<string, IUserAgent>();
        }

        private UserSegment mUserSegment = new UserSegment(5000);

        private  Queue<MessageToken> mQueue = new Queue<MessageToken>();

        public string ID
        {
            get;
            set;
        }

        public IList<IDesk> Desks
        {
            get;
            private set;
        }

        public void Add(IDesk desk)
        {
            // TODO: Implement this method
            desk.Room = this;
            Desks.Add(desk);
        }

        public string Password
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public IDictionary<string, IUserAgent> Users
        {
            get;
            private set;
        }

        public void In(IUserAgent user)
        {
            if (user.Desk != null)
                user.Desk.Exit(user);
            if (user.Room != null)
            {
                user.Room.Exit(user);
            }
            lock (Users)
            {
                Users[user.ID] = user;
                System.Threading.Interlocked.Increment(ref mVersion);
            }
            user.Room = this;
           
        }

        public void Exit(IUserAgent user)
        {
            if (user.Room == this)
            {
                user.Desk = null;
                user.Seat = null;
                user.Room = null;
                lock (Users)
                {
                    Users.Remove(user.ID);
                    System.Threading.Interlocked.Increment(ref mVersion);
                }
            }
        }

        public void AddMessage(IMessage message, IUserAgent user)
        {
            lock (this)
            {
                mQueue.Enqueue(new MessageToken { Message= message, UserAgent= user });
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

        protected virtual void OnExecute(MessageToken token, ISiqiServer server)
        {
            //添加处理逻辑
        }

        public ArraySegment<IUserAgent> GetAgents()
        {
            if (mUserSegment.Version != Version)
            {
                lock (Users)
                {
                    mUserSegment.Reset();
                    foreach (IUserAgent agent in Users.Values)
                    {
                        mUserSegment.Enqueue(agent);
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

        public RoomInfo GetInfo()
        {
            return new RoomInfo { ID = ID, Name = Name, Count = Users.Count };
        }

        public RoomInfoDetail GetInfoDetail()
        {
            RoomInfoDetail result = new RoomInfoDetail();
            result.RoomID = ID;
            result.RoomName = Name;
            result.Desks = new List<Protocol.DeskInfo>();
            for (int i = 0; i < Desks.Count; i++)
            {
                result.Desks.Add(Desks[i].GetInfo());
            }
            return result;
        }
    }
}
