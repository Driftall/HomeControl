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
            this.labelServerStatus = new System.Windows.Forms.Label();
            this.buttonServerDisconnect = new System.Windows.Forms.Button();
            this.buttonServerConnect = new System.Windows.Forms.Button();
            this.groupBoxAbout = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxEvents = new System.Windows.Forms.GroupBox();
            this.groupBoxOutputs = new System.Windows.Forms.GroupBox();
            this.textBoxNewOutputName = new System.Windows.Forms.TextBox();
            this.buttonAddOutput = new System.Windows.Forms.Button();
            this.textBoxNewOutput = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonOutputListRefresh = new System.Windows.Forms.Button();
            this.comboBoxOutputs = new System.Windows.Forms.ComboBox();
            this.groupBoxInputs = new System.Windows.Forms.GroupBox();
            this.textBoxNewInputName = new System.Windows.Forms.TextBox();
            this.buttonAddInput = new System.Windows.Forms.Button();
            this.textBoxNewInput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonInputListRefresh = new System.Windows.Forms.Button();
            this.comboBoxInputs = new System.Windows.Forms.ComboBox();
            this.buttonClientRefresh = new System.Windows.Forms.Button();
            this.comboBoxClients = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxServer.SuspendLayout();
            this.groupBoxAbout.SuspendLayout();
            this.groupBoxEvents.SuspendLayout();
            this.groupBoxOutputs.SuspendLayout();
            this.groupBoxInputs.SuspendLayout();
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
            this.textBoxIP.Size = new System.Drawing.Size(81, 20);
            this.textBoxIP.TabIndex = 1;
            this.textBoxIP.Text = "127.0.0.1";
            // 
            // groupBoxServer
            // 
            this.groupBoxServer.Controls.Add(this.labelServerStatus);
            this.groupBoxServer.Controls.Add(this.buttonServerDisconnect);
            this.groupBoxServer.Controls.Add(this.buttonServerConnect);
            this.groupBoxServer.Controls.Add(this.label1);
            this.groupBoxServer.Controls.Add(this.textBoxIP);
            this.groupBoxServer.Location = new System.Drawing.Point(12, 12);
            this.groupBoxServer.Name = "groupBoxServer";
            this.groupBoxServer.Size = new System.Drawing.Size(302, 79);
            this.groupBoxServer.TabIndex = 2;
            this.groupBoxServer.TabStop = false;
            this.groupBoxServer.Text = "Server Connection";
            // 
            // labelServerStatus
            // 
            this.labelServerStatus.AutoSize = true;
            this.labelServerStatus.Location = new System.Drawing.Point(170, 46);
            this.labelServerStatus.Name = "labelServerStatus";
            this.labelServerStatus.Size = new System.Drawing.Size(109, 13);
            this.labelServerStatus.TabIndex = 9;
            this.labelServerStatus.Text = "Status: Disconnected";
            // 
            // buttonServerDisconnect
            // 
            this.buttonServerDisconnect.Location = new System.Drawing.Point(226, 17);
            this.buttonServerDisconnect.Name = "buttonServerDisconnect";
            this.buttonServerDisconnect.Size = new System.Drawing.Size(70, 20);
            this.buttonServerDisconnect.TabIndex = 8;
            this.buttonServerDisconnect.Text = "Disconnect";
            this.buttonServerDisconnect.UseVisualStyleBackColor = true;
            this.buttonServerDisconnect.Click += new System.EventHandler(this.buttonServerDisconnect_Click);
            // 
            // buttonServerConnect
            // 
            this.buttonServerConnect.Location = new System.Drawing.Point(166, 17);
            this.buttonServerConnect.Name = "buttonServerConnect";
            this.buttonServerConnect.Size = new System.Drawing.Size(59, 20);
            this.buttonServerConnect.TabIndex = 7;
            this.buttonServerConnect.Text = "Connect";
            this.buttonServerConnect.UseVisualStyleBackColor = true;
            this.buttonServerConnect.Click += new System.EventHandler(this.buttonServerConnect_Click);
            // 
            // groupBoxAbout
            // 
            this.groupBoxAbout.Controls.Add(this.label3);
            this.groupBoxAbout.Location = new System.Drawing.Point(12, 320);
            this.groupBoxAbout.Name = "groupBoxAbout";
            this.groupBoxAbout.Size = new System.Drawing.Size(211, 53);
            this.groupBoxAbout.TabIndex = 3;
            this.groupBoxAbout.TabStop = false;
            this.groupBoxAbout.Text = "About";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "Home Control Suite - Windows Tray v0.1\r\n(c) Driftall 2013";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(229, 347);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(85, 26);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(229, 320);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(85, 26);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // groupBoxEvents
            // 
            this.groupBoxEvents.Controls.Add(this.groupBoxOutputs);
            this.groupBoxEvents.Controls.Add(this.groupBoxInputs);
            this.groupBoxEvents.Controls.Add(this.buttonClientRefresh);
            this.groupBoxEvents.Controls.Add(this.comboBoxClients);
            this.groupBoxEvents.Controls.Add(this.label4);
            this.groupBoxEvents.Location = new System.Drawing.Point(12, 97);
            this.groupBoxEvents.Name = "groupBoxEvents";
            this.groupBoxEvents.Size = new System.Drawing.Size(302, 217);
            this.groupBoxEvents.TabIndex = 6;
            this.groupBoxEvents.TabStop = false;
            this.groupBoxEvents.Text = "Events";
            // 
            // groupBoxOutputs
            // 
            this.groupBoxOutputs.Controls.Add(this.textBoxNewOutputName);
            this.groupBoxOutputs.Controls.Add(this.buttonAddOutput);
            this.groupBoxOutputs.Controls.Add(this.textBoxNewOutput);
            this.groupBoxOutputs.Controls.Add(this.label8);
            this.groupBoxOutputs.Controls.Add(this.buttonOutputListRefresh);
            this.groupBoxOutputs.Controls.Add(this.comboBoxOutputs);
            this.groupBoxOutputs.Location = new System.Drawing.Point(9, 139);
            this.groupBoxOutputs.Name = "groupBoxOutputs";
            this.groupBoxOutputs.Size = new System.Drawing.Size(287, 72);
            this.groupBoxOutputs.TabIndex = 4;
            this.groupBoxOutputs.TabStop = false;
            this.groupBoxOutputs.Text = "Outputs";
            // 
            // textBoxNewOutputName
            // 
            this.textBoxNewOutputName.Location = new System.Drawing.Point(6, 46);
            this.textBoxNewOutputName.Name = "textBoxNewOutputName";
            this.textBoxNewOutputName.Size = new System.Drawing.Size(87, 20);
            this.textBoxNewOutputName.TabIndex = 15;
            // 
            // buttonAddOutput
            // 
            this.buttonAddOutput.Location = new System.Drawing.Point(206, 44);
            this.buttonAddOutput.Name = "buttonAddOutput";
            this.buttonAddOutput.Size = new System.Drawing.Size(75, 23);
            this.buttonAddOutput.TabIndex = 14;
            this.buttonAddOutput.Text = "Add";
            this.buttonAddOutput.UseVisualStyleBackColor = true;
            // 
            // textBoxNewOutput
            // 
            this.textBoxNewOutput.Location = new System.Drawing.Point(93, 46);
            this.textBoxNewOutput.Name = "textBoxNewOutput";
            this.textBoxNewOutput.Size = new System.Drawing.Size(103, 20);
            this.textBoxNewOutput.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "List:";
            // 
            // buttonOutputListRefresh
            // 
            this.buttonOutputListRefresh.Location = new System.Drawing.Point(206, 18);
            this.buttonOutputListRefresh.Name = "buttonOutputListRefresh";
            this.buttonOutputListRefresh.Size = new System.Drawing.Size(75, 21);
            this.buttonOutputListRefresh.TabIndex = 10;
            this.buttonOutputListRefresh.Text = "Refresh";
            this.buttonOutputListRefresh.UseVisualStyleBackColor = true;
            // 
            // comboBoxOutputs
            // 
            this.comboBoxOutputs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOutputs.FormattingEnabled = true;
            this.comboBoxOutputs.Items.AddRange(new object[] {
            "PiServer",
            "Arduino_Uno",
            "BLAKE-PC",
            "BLAKE-LAPTOP"});
            this.comboBoxOutputs.Location = new System.Drawing.Point(44, 19);
            this.comboBoxOutputs.Name = "comboBoxOutputs";
            this.comboBoxOutputs.Size = new System.Drawing.Size(152, 21);
            this.comboBoxOutputs.TabIndex = 9;
            // 
            // groupBoxInputs
            // 
            this.groupBoxInputs.Controls.Add(this.textBoxNewInputName);
            this.groupBoxInputs.Controls.Add(this.buttonAddInput);
            this.groupBoxInputs.Controls.Add(this.textBoxNewInput);
            this.groupBoxInputs.Controls.Add(this.label5);
            this.groupBoxInputs.Controls.Add(this.buttonInputListRefresh);
            this.groupBoxInputs.Controls.Add(this.comboBoxInputs);
            this.groupBoxInputs.Location = new System.Drawing.Point(9, 46);
            this.groupBoxInputs.Name = "groupBoxInputs";
            this.groupBoxInputs.Size = new System.Drawing.Size(287, 87);
            this.groupBoxInputs.TabIndex = 3;
            this.groupBoxInputs.TabStop = false;
            this.groupBoxInputs.Text = "Inputs";
            // 
            // textBoxNewInputName
            // 
            this.textBoxNewInputName.Location = new System.Drawing.Point(6, 53);
            this.textBoxNewInputName.Name = "textBoxNewInputName";
            this.textBoxNewInputName.Size = new System.Drawing.Size(87, 20);
            this.textBoxNewInputName.TabIndex = 9;
            // 
            // buttonAddInput
            // 
            this.buttonAddInput.Location = new System.Drawing.Point(206, 51);
            this.buttonAddInput.Name = "buttonAddInput";
            this.buttonAddInput.Size = new System.Drawing.Size(75, 23);
            this.buttonAddInput.TabIndex = 8;
            this.buttonAddInput.Text = "Add";
            this.buttonAddInput.UseVisualStyleBackColor = true;
            // 
            // textBoxNewInput
            // 
            this.textBoxNewInput.Location = new System.Drawing.Point(93, 53);
            this.textBoxNewInput.Name = "textBoxNewInput";
            this.textBoxNewInput.Size = new System.Drawing.Size(103, 20);
            this.textBoxNewInput.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "List:";
            // 
            // buttonInputListRefresh
            // 
            this.buttonInputListRefresh.Location = new System.Drawing.Point(206, 19);
            this.buttonInputListRefresh.Name = "buttonInputListRefresh";
            this.buttonInputListRefresh.Size = new System.Drawing.Size(75, 21);
            this.buttonInputListRefresh.TabIndex = 4;
            this.buttonInputListRefresh.Text = "Refresh";
            this.buttonInputListRefresh.UseVisualStyleBackColor = true;
            // 
            // comboBoxInputs
            // 
            this.comboBoxInputs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInputs.FormattingEnabled = true;
            this.comboBoxInputs.Items.AddRange(new object[] {
            "PiServer",
            "Arduino_Uno",
            "BLAKE-PC",
            "BLAKE-LAPTOP"});
            this.comboBoxInputs.Location = new System.Drawing.Point(44, 20);
            this.comboBoxInputs.Name = "comboBoxInputs";
            this.comboBoxInputs.Size = new System.Drawing.Size(152, 21);
            this.comboBoxInputs.TabIndex = 3;
            // 
            // buttonClientRefresh
            // 
            this.buttonClientRefresh.Location = new System.Drawing.Point(221, 19);
            this.buttonClientRefresh.Name = "buttonClientRefresh";
            this.buttonClientRefresh.Size = new System.Drawing.Size(75, 21);
            this.buttonClientRefresh.TabIndex = 2;
            this.buttonClientRefresh.Text = "Refresh";
            this.buttonClientRefresh.UseVisualStyleBackColor = true;
            // 
            // comboBoxClients
            // 
            this.comboBoxClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClients.FormattingEnabled = true;
            this.comboBoxClients.Items.AddRange(new object[] {
            "PiServer",
            "Arduino_Uno",
            "BLAKE-PC",
            "BLAKE-LAPTOP"});
            this.comboBoxClients.Location = new System.Drawing.Point(53, 19);
            this.comboBoxClients.Name = "comboBoxClients";
            this.comboBoxClients.Size = new System.Drawing.Size(162, 21);
            this.comboBoxClients.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Client:";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 380);
            this.Controls.Add(this.groupBoxEvents);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxAbout);
            this.Controls.Add(this.groupBoxServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSettings";
            this.Text = "Home Control Suite - Settings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.groupBoxServer.ResumeLayout(false);
            this.groupBoxServer.PerformLayout();
            this.groupBoxAbout.ResumeLayout(false);
            this.groupBoxAbout.PerformLayout();
            this.groupBoxEvents.ResumeLayout(false);
            this.groupBoxEvents.PerformLayout();
            this.groupBoxOutputs.ResumeLayout(false);
            this.groupBoxOutputs.PerformLayout();
            this.groupBoxInputs.ResumeLayout(false);
            this.groupBoxInputs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.GroupBox groupBoxServer;
        private System.Windows.Forms.GroupBox groupBoxAbout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox groupBoxEvents;
        private System.Windows.Forms.ComboBox comboBoxClients;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonClientRefresh;
        private System.Windows.Forms.GroupBox groupBoxOutputs;
        private System.Windows.Forms.Button buttonAddOutput;
        private System.Windows.Forms.TextBox textBoxNewOutput;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonOutputListRefresh;
        private System.Windows.Forms.ComboBox comboBoxOutputs;
        private System.Windows.Forms.GroupBox groupBoxInputs;
        private System.Windows.Forms.Button buttonAddInput;
        private System.Windows.Forms.TextBox textBoxNewInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonInputListRefresh;
        private System.Windows.Forms.ComboBox comboBoxInputs;
        private System.Windows.Forms.TextBox textBoxNewOutputName;
        private System.Windows.Forms.TextBox textBoxNewInputName;
        private System.Windows.Forms.Button buttonServerConnect;
        private System.Windows.Forms.Button buttonServerDisconnect;
        private System.Windows.Forms.Label labelServerStatus;
    }
}

