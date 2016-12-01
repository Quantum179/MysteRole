using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysterole
{
    public class NotificationQuete : Notification
    {

        private Quete _queteNotifiee;
        public Quete QueteNotifiee
        {
            get { return _queteNotifiee; }
        }

        public NotificationQuete(Quete q)
        {
            _type = TypeNotification.Quete;
            _queteNotifiee = q;
        }



    }
}
