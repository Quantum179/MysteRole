using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mysterole
{
    public class Dialogue : Evenement 
    {
        public string Message;


        public Dialogue(string message) : base()
        {
            Message = message;
        }

        public override string GetTypeEvent()
        {
            return "Dialogue";
        }


    }
}
