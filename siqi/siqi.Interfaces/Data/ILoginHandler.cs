using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.Interfaces.Data
{
    public interface ILoginHandler
    {
        Model.User Login(string email, string pwd);

        void History(string email, string ip);
    }

    public class LoginHanlder : ILoginHandler
    {

        public Model.User Login(string email, string pwd)
        {
            return new Model.User { ID= email, Name= email };
        }

        public void History(string email, string ip)
        {
           
        }
    }
}
