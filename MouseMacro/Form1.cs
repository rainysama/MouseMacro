using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Helper;

namespace MouseMacro
{
    public partial class Form1 : Form
    {
        private MouseButtons currentMouseButton = MouseButtons.XButton2;
        private bool isRunning = false;
        private Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread = new Thread(new Helper.MouseMacro(this.GetSelectedProcessName, this.GetRepeatedMouseCount, this.GetCurrentMouseButton, this.IsRunning).Loop);
            thread.Start();

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

        private void RefreshProcessList()
        {
            processListBox.Items.Clear();            
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
                processListBox.Items.Add(p.ProcessName);            
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

            thread.Abort();
        }

        private string GetSelectedProcessName()
        {
            return processListBox.SelectedItem?.ToString();
        }

        private int GetRepeatedMouseCount()
        {
            return (int)repeatMouseCount.Value;
        }

        private MouseButtons GetCurrentMouseButton()
        {
            return currentMouseButton;
        }

        private bool IsRunning() => isRunning;
    }
}
