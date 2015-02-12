using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace siqi.AppConsole
{
   
    public class Program
    {
        private static EC.IApplication mApp;
        static void Main(string[] args)
        {
            mApp = EC.ECServer.Open("ecApplicatonSection");
            System.Threading.Thread.Sleep(-1);
        }
       
    }
  
}
