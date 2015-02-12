using EC;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Protocol
{
    public class IDVALUE
    {
        public const short LOGIN = 0X0001;
        public const short LOGIN_RESPONSE = 0X0002;


        public const short GET_ROOM = 0X0003;
        public const short GET_ROOM_RESPONSE = 0X0004;

        public const short LIST_ROOM = 0X0005;
        public const short LIST_ROOM_RESPONSE = 0X0006;

        public const short GET_DESK = 0X0007;
        public const short GET_DESK_REDPONSE = 0X0008;

        public const short LIST_DESK = 0X0009;
        public const short LIST_DESK_REDPONSE = 0X0010;

        public const short SELECT_ROOM = 0X0011;
        public const short SELECT_ROOM_RESPONSE = 0X0012;

        public const short SELECT_DESK = 0X0013;
        public const short SELECT_DESK_RESPONSE = 0X0014;

        public const short INFO_ROOM = 0X0901;
        public const short INFO_DESK = 0X0902;
        public const short INFO_SEAT = 0X0903;

        public const short RESULT = 0X1000;
        public const short TALK = 0X1001;

    }
}
