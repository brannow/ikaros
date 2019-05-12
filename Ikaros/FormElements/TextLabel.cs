using System.Windows.Forms;

namespace Ikaros.FormElements
{
    public class TextLabel: System.Windows.Forms.Label
    {
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0201://WM_LBUTTONDOWN
                    {
                        return;
                    }
                case 0x0202://WM_LBUTTONUP
                    {
                        return;
                    }
                case 0x0203://WM_LBUTTONDBLCLK
                    {
                        return;
                    }
                case 0x0204://WM_RBUTTONDOWN
                    {
                        return;
                    }
                case 0x0205://WM_RBUTTONUP
                    {
                        return;
                    }
                case 0x0206://WM_RBUTTONDBLCLK
                    {
                        return;
                    }
            }
            base.WndProc(ref m);
        }
    }
}
