using System.Runtime.InteropServices;

namespace CapsCommander
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        const uint KEYEVENTF_KEYUP = 0x2;
        bool CLEnabled = true;

        public Form1()
        {
            InitializeComponent();
            notifyIcon1.Text = "DoubleClick to Toggle CapsLock Commander\nRight Click to Exit";
            DoCapsLock();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!CLEnabled) return;

            bool CapsLock = Control.IsKeyLocked(Keys.CapsLock);
            //bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            //bool NumLock = (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
            //bool ScrollLock = (((ushort)GetKeyState(0x91)) & 0xffff) != 0;

            if (CapsLock)
            {
                keybd_event((byte)Keys.CapsLock, 0, 0, 0);
                keybd_event((byte)Keys.CapsLock, 0, KEYEVENTF_KEYUP, 0);
            }
        }

        void DoCapsLock()
        {
            if (CLEnabled)
            {
                notifyIcon1.Icon = Icon.FromHandle(((Bitmap)imageList1.Images[4]).GetHicon());
            }
            else
            {
                notifyIcon1.Icon = Icon.FromHandle(((Bitmap)imageList1.Images[2]).GetHicon());

            }
        }


        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CLEnabled = !CLEnabled;
            DoCapsLock();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if(me.Button == MouseButtons.Right)
            {
                Application.Exit();
            }
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}