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

        private int m_remainCountToClick = 0;
        private uint m_mouseX, m_mouseY = 0;

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
                {
                    if (m_remainCountToClick > 1)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, m_mouseX, m_mouseY, 0, 0);
                        m_remainCountToClick--;                        
                        Thread.Sleep(100);
                    }
                }
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
                m_mouseX = (uint)e.X;
                m_mouseY = (uint)e.Y;
                m_remainCountToClick += m_getRepeatedMouseCount();
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