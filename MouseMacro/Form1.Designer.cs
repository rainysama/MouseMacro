using System;

namespace MouseMacro
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.processListBox = new System.Windows.Forms.ListBox();
            this.repeatProcessCountLabel = new System.Windows.Forms.Label();
            this.repeatMouseCount = new System.Windows.Forms.NumericUpDown();
            this.mouseButtonName = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.mouseNameCombobBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.repeatMouseCount)).BeginInit();
            this.SuspendLayout();
            // 
            // processListBox
            // 
            this.processListBox.FormattingEnabled = true;
            this.processListBox.Location = new System.Drawing.Point(12, 12);
            this.processListBox.Name = "processListBox";
            this.processListBox.Size = new System.Drawing.Size(229, 329);
            this.processListBox.TabIndex = 0;
            // 
            // repeatProcessCountLabel
            // 
            this.repeatProcessCountLabel.AutoSize = true;
            this.repeatProcessCountLabel.Location = new System.Drawing.Point(12, 360);
            this.repeatProcessCountLabel.Name = "repeatProcessCountLabel";
            this.repeatProcessCountLabel.Size = new System.Drawing.Size(116, 13);
            this.repeatProcessCountLabel.TabIndex = 1;
            this.repeatProcessCountLabel.Text = "İşlem tekrarlama miktarı";
            // 
            // repeatMouseCount
            // 
            this.repeatMouseCount.Location = new System.Drawing.Point(149, 360);
            this.repeatMouseCount.Name = "repeatMouseCount";
            this.repeatMouseCount.Size = new System.Drawing.Size(92, 20);
            this.repeatMouseCount.TabIndex = 2;
            // 
            // mouseButtonName
            // 
            this.mouseButtonName.AutoSize = true;
            this.mouseButtonName.Location = new System.Drawing.Point(12, 393);
            this.mouseButtonName.Name = "mouseButtonName";
            this.mouseButtonName.Size = new System.Drawing.Size(108, 13);
            this.mouseButtonName.TabIndex = 4;
            this.mouseButtonName.Text = "Kullanılacak fare tuşu";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(15, 426);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(105, 23);
            this.startBtn.TabIndex = 5;
            this.startBtn.Text = "Başlat";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(138, 426);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(103, 23);
            this.stopBtn.TabIndex = 6;
            this.stopBtn.Text = "Durdur";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // mouseNameCombobBox
            // 
            this.mouseNameCombobBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mouseNameCombobBox.FormattingEnabled = true;
            this.mouseNameCombobBox.Location = new System.Drawing.Point(149, 399);
            this.mouseNameCombobBox.Name = "mouseNameCombobBox";
            this.mouseNameCombobBox.Size = new System.Drawing.Size(91, 21);
            this.mouseNameCombobBox.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 461);
            this.Controls.Add(this.mouseNameCombobBox);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.mouseButtonName);
            this.Controls.Add(this.repeatMouseCount);
            this.Controls.Add(this.repeatProcessCountLabel);
            this.Controls.Add(this.processListBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(268, 500);
            this.Name = "Form1";
            this.Text = "Mouse Macro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repeatMouseCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox processListBox;
        private System.Windows.Forms.Label repeatProcessCountLabel;
        private System.Windows.Forms.NumericUpDown repeatMouseCount;
        private System.Windows.Forms.Label mouseButtonName;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.ComboBox mouseNameCombobBox;
    }
}

