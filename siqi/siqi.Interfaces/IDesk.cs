using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Interfaces
{
    public interface IDesk
    {
        long Version { get; }
        string ID { get; set; }

        string Name { get; set; }

        IList<ISeat> Seats { get; }

        IList<IUserAgent> Visitors { get; }

        void Add(ISeat seat);

        IRoom Room { get; set; }

        string In(IUserAgent user, int seatIndex);

        void Exit(IUserAgent user);

        void AddMessage(Protocol.IMessage message, IUserAgent user);

        void Execute(ISiqiServer server);

        ArraySegment<Interfaces.IUserAgent> GetAgents();

        Protocol.DeskInfo GetInfo();
    }
}
