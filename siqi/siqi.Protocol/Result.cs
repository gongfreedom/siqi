using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EC;
namespace siqi.Protocol
{
    [MessageID(IDVALUE.RESULT)]
    public class Result:MessageBase
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
