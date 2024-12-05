using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroVision
{
    public class NewNotificationEventArgs : EventArgs
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }

    }
}
