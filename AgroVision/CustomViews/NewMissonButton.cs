using System;
using System.Drawing;

namespace AgroVision.CustomViews
{
    public class CircleMissionButton
    {

        private event Action OnClick;
        public CircleMissionButton(string title, Action on_click)
        {
            Title = title;
            OnClick = on_click;
        }

        public string Title { get; set; }

        public Image Icon { get; set; }

        public RectangleF Rectangle { get; set; }

        public void PerformClick()
        {
            OnClick?.Invoke();
        }

    }
}
