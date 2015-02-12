using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EC;
namespace siqi.Protocol
{
    [MessageID(IDVALUE.LIST_ROOM)]
    public class ListRoom : MessageBase
    {

    }

    [MessageID(IDVALUE.LIST_ROOM_RESPONSE)]
    public class ListRoomResponse : Result
    {
        public IList<RoomInfo> Items
        {
            get;
            set;
        }
    }
    [MessageID(IDVALUE.INFO_ROOM)]
    public class RoomInfo
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }

    //info detail
    [MessageID(IDVALUE.GET_ROOM)]
    public class GetRoom : MessageBase
    {
        public string Room { get; set; }

    }
    [MessageID(IDVALUE.GET_ROOM_RESPONSE)]
    public class RoomInfoDetail : MessageBase
    {
        public string RoomID { get; set; }
        public string RoomName { get; set; }
        public IList<DeskInfo> Desks { get; set; }
    }
    [MessageID(IDVALUE.INFO_DESK)]
    public class DeskInfo : MessageBase
    {
        public string DeskID { get; set; }
        public string Name { get; set; }
        public IList<SeatInfo> Seats { get; set; }
    }
    [MessageID(IDVALUE.INFO_SEAT)]
    public class SeatInfo : MessageBase
    {
        public int Index { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
    }

    //select room
    [MessageID(IDVALUE.SELECT_ROOM)]
    public class SelectRoom : MessageBase
    {
        public string Room { get; set; }
    }
    [MessageID(IDVALUE.SELECT_ROOM_RESPONSE)]
    public class SelectRoomResponse : Result
    {
        public string Room { get; set; }
    }

    //select desk
    [MessageID(IDVALUE.SELECT_DESK)]
    public class SelectDesk : MessageBase
    {
        public string Room { get; set; }
        public string Desk { get; set; }
    }
     [MessageID(IDVALUE.SELECT_DESK_RESPONSE)]
    public class SelectDeskResponse:Result{
        public string Desk{get;set;}
    }


    //talk
    [MessageID(IDVALUE.TALK)]
    public class Talk : MessageBase
    {
        public string To
        {
            get;
            set;
        }
        public string Content { get; set; }
    }
}
