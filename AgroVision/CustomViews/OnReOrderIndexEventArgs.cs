using System;

namespace AgroVision.CustomViews
{
    public class OnReOrderIndexEventArgs : EventArgs
    {

        public int OldIndex { get; set; }
        public int NewIndex { get; set; }

    }
}
