using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EC;
namespace siqi.Interfaces
{
    public interface IUserAgent
    {
        string Name { get; set; }

        string ID { get; set; }

        ISession Session { get; set; }

        IRoom Room { get; set; }

        IDesk Desk { get; set; }

        ISeat Seat { get; set; }
    }
}
