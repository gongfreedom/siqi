using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using siqi.Interfaces;
using siqi.Interfaces.Data;
using siqi.Logic.Dispatch;

namespace siqi.Logic
{
    [EC.Controller]
    public class SiqiServer:Interfaces.ISiqiServer
    {

        public SiqiServer()
        {
            Users = new Dictionary<string, Interfaces.IUserAgent>();
            Rooms = new List<Interfaces.IRoom>();
            mDispatchFactory = new DispatchFactory();
            mDispatchFactory.Initialize();
         


            LoginHandler = new siqi.Interfaces.Data.LoginHanlder();

            AppModel.Initialize(this);
            
        }

        private DispatchFactory mDispatchFactory;
       

        private Interfaces.IUserAgent GetAgent(EC.ISession session)
        {
            return (Interfaces.IUserAgent)session[SESSION_KEY.USER];
        }

        private Dispatch.MessageToken CreateToken(Protocol.IMessage message, EC.ISession session)
        {
            return new Dispatch.MessageToken { Message = message, UserAgent = (IUserAgent)session[SESSION_KEY.USER], Server=this,
             Session= session};
        }

        public ILoginHandler LoginHandler
        {
            get;
            set;
        }

        public IDictionary<string, Interfaces.IUserAgent> Users { get;private set; }

        public List<Interfaces.IRoom> Rooms
        {
            get;
            private set;
        }

        public void AddUser(IUserAgent agent)
        {
            Users[agent.ID] = agent;
        }

        public IUserAgent GetUser(string id)
        {
            IUserAgent result=null;
            Users.TryGetValue(id, out result);
            return result;
                
        }

        public Protocol.ListRoomResponse ListRoom()
        {
            Protocol.ListRoomResponse result = new Protocol.ListRoomResponse();
            result.Items = new List<Protocol.RoomInfo>();
            foreach (Interfaces.IRoom room in Rooms)
            {
                result.Items.Add(room.GetInfo());
            }
            result.Success = true;
            return result;
        }

        public void Add(IRoom room)
        {
            Rooms.Add(room);
        }

        public void Login(EC.ISession session, Protocol.Login e)
        {
            mDispatchFactory.Route(CreateToken(e,session));
        }

        public void SelectRoom(EC.ISession session, Protocol.SelectRoom e)
        {
            mDispatchFactory.Route(CreateToken(e, session));
        }

        public void SelectDesk(EC.ISession session, Protocol.SelectDesk e)
        {
            mDispatchFactory.Route(CreateToken(e, session));
        }

        public void ListRoom(EC.ISession session, Protocol.ListRoom e)
        {
            mDispatchFactory.Route(CreateToken(e, session));
        }

        public void Talk(EC.ISession session, Protocol.Talk e)
        {
            
            mDispatchFactory.Route(CreateToken(e, session));
        }


        public EC.IApplication Application
        {
            get
            {
                return AppModel.Application;
            }
            set
            {
            }
        }

        public void Send(Protocol.IMessage message, params Interfaces.IUserAgent[] agents)
        {
            for (int i = 0; i < agents.Length; i++)
                Send(message, agents[i].Session.Channel);
        }

        public void Send(Protocol.IMessage message, ArraySegment<Interfaces.IUserAgent> agents)
        {
            for (int i = 0; i < agents.Count; i++)
                Send(message, agents.Array[i].Session.Channel);
        }

        public void Send(Protocol.IMessage message, params EC.ISession[] session)
        {
            for (int i = 0; i < session.Length; i++)
                Send(message, session[i].Channel);
        }
        
        public void Send(Protocol.IMessage message, Beetle.Express.IChannel channel)
        {
            Application.Server.Send(message, channel);
        }


        public IRoom GetRoom(string roomid)
        {
            return Rooms.FirstOrDefault(o => o.ID == roomid);
        }
    }
}
