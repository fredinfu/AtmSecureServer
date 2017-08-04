namespace FtpServerUI 
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbHashServer = new System.Windows.Forms.Label();
            this.lbHashClient = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rtTramaEntranteEncrypt = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtTramaEntranteDesc = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtTramaSalienteEncrypt = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rtTramaSalienteDesc = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.lboxUsuariosConectados = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(795, 515);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbHashServer);
            this.tabPage1.Controls.Add(this.lbHashClient);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.rtTramaEntranteEncrypt);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.rtTramaEntranteDesc);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(787, 486);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "JSON in";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbHashServer
            // 
            this.lbHashServer.AutoSize = true;
            this.lbHashServer.Location = new System.Drawing.Point(380, 373);
            this.lbHashServer.Name = "lbHashServer";
            this.lbHashServer.Size = new System.Drawing.Size(20, 17);
            this.lbHashServer.TabIndex = 7;
            this.lbHashServer.Text = "...";
            // 
            // lbHashClient
            // 
            this.lbHashClient.AutoSize = true;
            this.lbHashClient.Location = new System.Drawing.Point(6, 373);
            this.lbHashClient.Name = "lbHashClient";
            this.lbHashClient.Size = new System.Drawing.Size(20, 17);
            this.lbHashClient.TabIndex = 6;
            this.lbHashClient.Text = "...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(380, 356);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Hash:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 356);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Hash:";
            // 
            // rtTramaEntranteEncrypt
            // 
            this.rtTramaEntranteEncrypt.Enabled = false;
            this.rtTramaEntranteEncrypt.Location = new System.Drawing.Point(383, 23);
            this.rtTramaEntranteEncrypt.Name = "rtTramaEntranteEncrypt";
            this.rtTramaEntranteEncrypt.Size = new System.Drawing.Size(371, 330);
            this.rtTramaEntranteEncrypt.TabIndex = 3;
            this.rtTramaEntranteEncrypt.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Entrada JSON";
            // 
            // rtTramaEntranteDesc
            // 
            this.rtTramaEntranteDesc.Enabled = false;
            this.rtTramaEntranteDesc.Location = new System.Drawing.Point(6, 23);
            this.rtTramaEntranteDesc.Name = "rtTramaEntranteDesc";
            this.rtTramaEntranteDesc.Size = new System.Drawing.Size(371, 330);
            this.rtTramaEntranteDesc.TabIndex = 1;
            this.rtTramaEntranteDesc.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtTramaSalienteEncrypt);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.rtTramaSalienteDesc);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(787, 486);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "JSON out";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtTramaSalienteEncrypt
            // 
            this.rtTramaSalienteEncrypt.Location = new System.Drawing.Point(383, 23);
            this.rtTramaSalienteEncrypt.Name = "rtTramaSalienteEncrypt";
            this.rtTramaSalienteEncrypt.Size = new System.Drawing.Size(371, 330);
            this.rtTramaSalienteEncrypt.TabIndex = 4;
            this.rtTramaSalienteEncrypt.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Salida JSON";
            // 
            // rtTramaSalienteDesc
            // 
            this.rtTramaSalienteDesc.Location = new System.Drawing.Point(6, 23);
            this.rtTramaSalienteDesc.Name = "rtTramaSalienteDesc";
            this.rtTramaSalienteDesc.Size = new System.Drawing.Size(371, 330);
            this.rtTramaSalienteDesc.TabIndex = 2;
            this.rtTramaSalienteDesc.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.lboxUsuariosConectados);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(787, 486);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Usuarios Conectados";
            // 
            // lboxUsuariosConectados
            // 
            this.lboxUsuariosConectados.FormattingEnabled = true;
            this.lboxUsuariosConectados.ItemHeight = 16;
            this.lboxUsuariosConectados.Location = new System.Drawing.Point(6, 23);
            this.lboxUsuariosConectados.Name = "lboxUsuariosConectados";
            this.lboxUsuariosConectados.Size = new System.Drawing.Size(432, 324);
            this.lboxUsuariosConectados.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(444, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 341);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conectar Servidor";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 41);
            this.button1.TabIndex = 0;
            this.button1.Text = "Encender";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(114, 196);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 41);
            this.button2.TabIndex = 1;
            this.button2.Text = "Apagar";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 539);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "ATM Server";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox rtTramaEntranteDesc;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox lboxUsuariosConectados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtTramaSalienteDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtTramaEntranteEncrypt;
        private System.Windows.Forms.RichTextBox rtTramaSalienteEncrypt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbHashServer;
        private System.Windows.Forms.Label lbHashClient;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

