using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Interfaces
{
    public interface IRoom
    {
        long Version { get; }

        string ID { get; set; }

        IList<IDesk> Desks { get; }

        void Add(IDesk desk);

        string Password { get; set; }

        string Name { get; set; }

        IDictionary<string, IUserAgent> Users { get; }

        void In(IUserAgent user);

        void Exit(IUserAgent user);

        void AddMessage(Protocol.IMessage message,IUserAgent user);

        void Execute(ISiqiServer server);

        ArraySegment<Interfaces.IUserAgent> GetAgents();

        Protocol.RoomInfo GetInfo();

        Protocol.RoomInfoDetail GetInfoDetail();
        
    }
}
