using siqi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Logic
{
    public class UserSegment
    {
        public UserSegment(int count)
        {
            mUsers = new Interfaces.IUserAgent[count];
        }

        private int mPostion = 0;

        private Interfaces.IUserAgent[] mUsers;

        public long Version
        {
            get;
            set;
        }

        public void Reset()
        {
            mPostion = 0;
        }
        public void Enqueue(IUserAgent agent)
        {
            mUsers[mPostion] = agent;
            mPostion++;
        }

        public ArraySegment<IUserAgent> GetAgents()
        {
            return new ArraySegment<IUserAgent>(mUsers, 0, mPostion);
        }
    }
}
