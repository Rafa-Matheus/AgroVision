using System;
using System.Drawing;
using System.Windows.Forms;

namespace AgroVision.CustomViews
{
    public class ReOrderPanel : Panel
    {

        public event EventHandler<OnReOrderIndexEventArgs> OnReOrderIndex;

        private readonly Pen pen;
        public ReOrderPanel()
        {
            y = -1;

            SetStyle(ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw, true);

            pen = new Pen(Brushes.White, 3f);

            int oldIndex = -1;
            int newIndex = -1;
            MouseDown += (o, args) =>
            {
                if (args.Button == MouseButtons.Left)
                    for (int controlIndex = 0; controlIndex < Controls.Count; controlIndex++)
                    {
                        Control control = Controls[controlIndex];

                        if (new Rectangle(control.Left, control.Top, control.Width, control.Height).Contains(args.Location))
                        {
                            oldIndex = controlIndex;
                            break;
                        }
                    }
            };
            MouseMove += (o, args) =>
            {
                if (args.Button == MouseButtons.Left)
                    for (int controlIndex = 0; controlIndex < Controls.Count; controlIndex++)
                    {
                        Control control = Controls[controlIndex];

                        Rectangle up_rect = new Rectangle(control.Left, control.Top, control.Width, control.Height / 2);
                        Rectangle down_rect = new Rectangle(control.Left, control.Top + control.Height / 2, control.Width, control.Height / 2);

                        newIndex = controlIndex;
                        if (up_rect.Contains(args.Location))
                        {
                            SetDrawLine(up_rect.Y);
                            break;
                        }
                        else if (down_rect.Contains(args.Location))
                        {
                            SetDrawLine(up_rect.Y + control.Height);
                            break;
                        }
                    }
            };
            MouseUp += delegate
            {
                if (oldIndex != newIndex)
                    if (oldIndex > -1 && newIndex > -1)
                    {
                        Controls.SetChildIndex(Controls[oldIndex], newIndex);

                        OnReOrderIndex?.Invoke(this, new OnReOrderIndexEventArgs() { OldIndex = oldIndex, NewIndex = newIndex });

                        oldIndex = newIndex = -1;
                    }

                SetDrawLine(-1);
            };
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            e.Control.MouseDown += Control_MouseDown;
            e.Control.MouseMove += OnMouseMove;
            e.Control.MouseUp += OnMouseUp;
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePoint = PointToClient(Cursor.Position);

                base.OnMouseDown(new MouseEventArgs(e.Button, e.Clicks, mousePoint.X, mousePoint.Y, e.Delta));
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mouse_p = PointToClient(Cursor.Position);

                base.OnMouseMove(new MouseEventArgs(e.Button, e.Clicks, mouse_p.X, mouse_p.Y, e.Delta));
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mouse_p = PointToClient(Cursor.Position);

                base.OnMouseUp(new MouseEventArgs(e.Button, e.Clicks, mouse_p.X, mouse_p.Y, e.Delta));
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            e.Control.MouseDown -= Control_MouseDown;
            e.Control.MouseMove -= OnMouseMove;
            e.Control.MouseUp -= OnMouseUp;
        }

        private int y;
        public void SetDrawLine(int y)
        {
            this.y = y;

            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (y > -1)
                e.Graphics.DrawLine(pen, new Point(0, y), new Point(ClientRectangle.Width, y));
        }

    }
}
