using System;

namespace Mysterole
{
	public class Dialogue : Evenement
	{
        //Attributs
        private string _message;

        //Propriétés
        public string Message
        {
            get { return _message; }
            private set { }
        }


        public Dialogue () : base()
		{
            _message = null;
		}
        public Dialogue(string m)
        {
            _message = m;
        }

        public bool LancerDialogue(string message)
        {

            return false;
        }
	}
}













/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mysterole
{
    public class Dialogue : Evenement 
    {
        private string _message;
        public string Message { get; private set; }


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
*/
