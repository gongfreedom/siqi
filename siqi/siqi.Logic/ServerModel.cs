using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EC;
namespace siqi.Logic
{
    public class AppModel : EC.IAppModel
    {
        public static EC.IApplication Application
        {
            get;
            set;
        }

        public void Init(EC.IApplication application)
        {
            Application = application;
            
            application.Disconnected += (o, e) =>
            {
                e.Session[SESSION_KEY.USER] = null;
            };
            application.Connected += (o, e) =>
            {

            };
            application.Error += (o, e) =>
            {
                "{0} error {1}".Log4Error(e.Info.Error, "", "");
            };
           
        }

        public static void Initialize(Interfaces.ISiqiServer server)
        {
            for (int i = 0; i < 10; i++)
            {
                string id= (i+1).ToString();
                Room room = new Room(id,id);
                for (int k = 0; k < 200; k++)
                {
                    id = (k + 1).ToString();
                    room.Add(new Desk(id, id, 4));
                }
                server.Add(room);
            }
        }

        public string Name
        {
            get { return "AppModel"; }
        }

        public string Command(string cmd)
        {
            return null;
        }
    }
}
