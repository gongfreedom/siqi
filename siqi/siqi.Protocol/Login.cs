using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Protocol
{
    [EC.MessageID(IDVALUE.LOGIN)]
    public class Login:MessageBase
    {
        public string EMail { get; set; }

        public string Password { get; set; }
    }
    [EC.MessageID(IDVALUE.LOGIN_RESPONSE)]
    public class LoginResponse : MessageBase
    {
        public string Tokey { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
