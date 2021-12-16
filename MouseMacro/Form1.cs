using Gma.System.MouseKeyHook;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseMacro
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();


        private IKeyboardMouseEvents m_GlobalHook;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private bool isRunning = false;
        private MouseButtons currentMouseButton = MouseButtons.XButton2;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseDownExt += OnMouseDown;

            stopBtn.Enabled = false;
            
            RefreshProcessList();
            RefreshMouseButtonNamesList();
        }

        private void RefreshMouseButtonNamesList()
        {            
            mouseNameCombobBox.Items.Clear();
            mouseNameCombobBox.Items.Add(MouseButtons.None);
            mouseNameCombobBox.Items.Add(MouseButtons.XButton1);
            mouseNameCombobBox.Items.Add(MouseButtons.XButton2);

            mouseNameCombobBox.SelectedIndex = mouseNameCombobBox.FindStringExact("XButton2");
        }

        private void OnMouseDown(object sender, MouseEventExtArgs e)
        {
            if (!isRunning)            
                return;
            
            var activatedProcessFileName = GetActiveProcessFileName();
            if (e.Button == currentMouseButton && activatedProcessFileName == processListBox.SelectedItem.ToString())
            {
                for (int i = 0; i < repeatMouseCount.Value; ++i)
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)e.X, (uint)e.Y, 0, 0);

                e.Handled = true;
            }            
        }

        private void RefreshProcessList()
        {
            processListBox.Items.Clear();            
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
                processListBox.Items.Add(p.ProcessName);
        }

        private string GetActiveProcessFileName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (repeatMouseCount.Value < 1)
            {
                MessageBox.Show("İşlem tekrar miktarı 1'den küçük olduğu için devam edilemez.", "Mouse Macro", MessageBoxButtons.OK);
                return;
            }

            if (processListBox.SelectedItem == null)
            {
                MessageBox.Show("İşlemin çalışacağı pencere belirtilmeden işleme devam edilemez.", "Mouse Macro", MessageBoxButtons.OK);
                return;
            }

            processListBox.Enabled = false;
            repeatMouseCount.Enabled = false;
            mouseNameCombobBox.Enabled = false;

            currentMouseButton = (MouseButtons)Enum.Parse(typeof(MouseButtons), mouseNameCombobBox.SelectedItem?.ToString());
            isRunning = true;
            startBtn.Enabled = false;
            stopBtn.Enabled = true;
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            processListBox.Enabled = true;
            repeatMouseCount.Enabled = true;
            mouseNameCombobBox.Enabled = true;

            isRunning = false;
            startBtn.Enabled = true;
            stopBtn.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRunning)
            {
                var result = MessageBox.Show("Şuanda mouse makro çalışmakta. Kapatmak istiyor musunuz?", "Mouse Macro", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }                
            }

            m_GlobalHook.MouseDownExt -= OnMouseDown;
            m_GlobalHook.Dispose();
        }
    }
}
