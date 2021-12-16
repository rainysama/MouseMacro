using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace Helper
{
    public class MouseMacro
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

        private Func<bool> m_isRunning;
        private Func<string> m_getSelectedProcessName;
        private Func<int> m_getRepeatedMouseCount;
        private Func<MouseButtons> m_getCurrentMouseButton;

        public MouseMacro(Func<string> getSelectedProcessName, Func<int> getrepeatedMouseCount, Func<MouseButtons> getCurrentMouseButton, Func<bool> isRunning)
        {
            m_getSelectedProcessName = getSelectedProcessName;
            m_getRepeatedMouseCount = getrepeatedMouseCount;
            m_getCurrentMouseButton = getCurrentMouseButton;
            m_isRunning = isRunning;

            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseDownExt += OnMouseDown;
        }

        ~MouseMacro()
        {
            Dispose();
        }

        public void Loop()
        {
            try
            {
                while (true)
                    continue;
            } catch (ThreadAbortException)
            {
                Dispose();
            }
        }

        private void OnMouseDown(object sender, MouseEventExtArgs e)
        {
            if (!m_isRunning())
                return;

            var activatedProcessFileName = GetActiveProcessFileName();

            if (e.Button == m_getCurrentMouseButton() && activatedProcessFileName == m_getSelectedProcessName())
            {                                
                for (int i = 0; i < m_getRepeatedMouseCount(); ++i)
                {                    
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)e.X, (uint)e.Y, 0, 0);
                }

                e.Handled = true;
            }
        }

        private string GetActiveProcessFileName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }

        private void Dispose()
        {
            m_GlobalHook.MouseDownExt -= OnMouseDown;
            m_GlobalHook.Dispose();
        }
    }
}