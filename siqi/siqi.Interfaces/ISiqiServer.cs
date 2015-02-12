using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Interfaces
{
    public interface ISiqiServer
    {

        EC.IApplication Application
        {
            get;
            set;
        }

        Data.ILoginHandler LoginHandler { get; set; }

        IDictionary<string, IUserAgent> Users { get; }

        List<IRoom> Rooms { get; }

        Protocol.ListRoomResponse ListRoom();

        void AddUser(IUserAgent agent);

        IUserAgent GetUser(string id);

        void Add(IRoom room);

        IRoom GetRoom(string roomid);

        void Send(Protocol.IMessage message, params Interfaces.IUserAgent[] agents);

        void Send(Protocol.IMessage message, ArraySegment<Interfaces.IUserAgent> agents);

        void Send(Protocol.IMessage message, params EC.ISession[] session);

        void Send(Protocol.IMessage message, Beetle.Express.IChannel channel);
    }
}
