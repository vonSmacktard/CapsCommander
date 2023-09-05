//CapsCommander by Baron von Smacktard, although it's really stupidly little code.
using System.Runtime.InteropServices;

namespace CapsCommander
{
    public partial class Form1 : Form
    {
        //we need this to stuff a keyboard press into the buffer to do what we need to do.
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        const uint KEYEVENTF_KEYUP = 0x2;

        //CLEnabled is toggled by double-clicking the trayicon
        //GREEN means enabled, RED means disabled
        bool CLEnabled = true;

        public Form1()
        {
            InitializeComponent();
            //set the tooltip hover-over text
            notifyIcon1.Text = "DoubleClick to Toggle CapsLock Commander\nRight Click to Exit";
            //set the trayicon icon
            DoCapsLock();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //If our capslock is disabled, just nevermind.
            if (!CLEnabled) return;

            //get CapsLock state
            bool CapsLock = Control.IsKeyLocked(Keys.CapsLock);
            //if it's on, turn that shit off by
            //simulating PRESS and UNPRESS of CapsLock Key
            if (CapsLock)
            {
                keybd_event((byte)Keys.CapsLock, 0, 0, 0);
                keybd_event((byte)Keys.CapsLock, 0, KEYEVENTF_KEYUP, 0);
            }
        }

        void DoCapsLock()
        {
            //This just sets the trayicon icon depending on whether or not
            //CapsCommander is enabled (turning off Capslock if it's on)
            //or disabled (um.. doing nothing)
            if (CLEnabled)
            {
                notifyIcon1.Icon = Icon.FromHandle(((Bitmap)imageList1.Images[4]).GetHicon());
            }
            else
            {
                notifyIcon1.Icon = Icon.FromHandle(((Bitmap)imageList1.Images[2]).GetHicon());

            }
        }


        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Toggle CapsLock mode
            CLEnabled = !CLEnabled;
            //Update trayicon icon
            DoCapsLock();
        }

        private void NotifyIcon1_Click(object sender, EventArgs e)
        {
            //right mouse click on trayicon icon exits program
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {
                Application.Exit();
            }
        }
        //that's it.  Nothing else to it.
    }
}