namespace WindowsTray
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.groupBoxServer = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBoxAbout = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxServer.SuspendLayout();
            this.groupBoxAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server IP:";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(79, 17);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(101, 20);
            this.textBoxIP.TabIndex = 1;
            this.textBoxIP.Text = "192.168.0.4";
            // 
            // groupBoxServer
            // 
            this.groupBoxServer.Controls.Add(this.textBox1);
            this.groupBoxServer.Controls.Add(this.label2);
            this.groupBoxServer.Controls.Add(this.label1);
            this.groupBoxServer.Controls.Add(this.textBoxIP);
            this.groupBoxServer.Location = new System.Drawing.Point(12, 12);
            this.groupBoxServer.Name = "groupBoxServer";
            this.groupBoxServer.Size = new System.Drawing.Size(196, 79);
            this.groupBoxServer.TabIndex = 2;
            this.groupBoxServer.TabStop = false;
            this.groupBoxServer.Text = "Server Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server Port:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(79, 43);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(101, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "6666";
            // 
            // groupBoxAbout
            // 
            this.groupBoxAbout.Controls.Add(this.label3);
            this.groupBoxAbout.Location = new System.Drawing.Point(214, 12);
            this.groupBoxAbout.Name = "groupBoxAbout";
            this.groupBoxAbout.Size = new System.Drawing.Size(128, 79);
            this.groupBoxAbout.TabIndex = 3;
            this.groupBoxAbout.TabStop = false;
            this.groupBoxAbout.Text = "About";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 39);
            this.label3.TabIndex = 0;
            this.label3.Text = "Home Control Suite\r\nWindows Tray v0.1\r\n(c) Driftall 2013";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 104);
            this.Controls.Add(this.groupBoxAbout);
            this.Controls.Add(this.groupBoxServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSettings";
            this.Text = "Home Control Suite - Settings";
            this.groupBoxServer.ResumeLayout(false);
            this.groupBoxServer.PerformLayout();
            this.groupBoxAbout.ResumeLayout(false);
            this.groupBoxAbout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.GroupBox groupBoxServer;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxAbout;
        private System.Windows.Forms.Label label3;
    }
}

