using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Moviebase.Views
{
    class DropDownButton : Button
    {
        [DefaultValue(null)]
        public ContextMenuStrip Menu { get; set; }

        [DefaultValue(false)]
        public bool ShowMenuUnderCursor { get; set; }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (Menu == null || mevent.Button != MouseButtons.Left) return;
            var menuLocation = ShowMenuUnderCursor ? mevent.Location : new Point(0, Height);
            Menu.Show(this, menuLocation);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            if (Menu != null)
            {
                int arrowX = ClientRectangle.Width - 14;
                int arrowY = ClientRectangle.Height / 2 - 1;

                Brush brush = Enabled ?Brushes.White  : Brushes.DarkSlateGray;
                Point[] arrows = { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
                pevent.Graphics.FillPolygon(brush, arrows);
            }
        }
    }
}
