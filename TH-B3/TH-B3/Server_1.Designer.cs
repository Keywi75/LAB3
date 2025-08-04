namespace UDP_Server
{
    partial class Server_1
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
            this.txtPort = new System.Windows.Forms.TextBox();
            this.Listen_Btn = new System.Windows.Forms.Button();
            this.listMessages = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(121, 38);
            this.txtPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(240, 35);
            this.txtPort.TabIndex = 0;
            // 
            // Listen_Btn
            // 
            this.Listen_Btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Listen_Btn.Location = new System.Drawing.Point(413, 32);
            this.Listen_Btn.Margin = new System.Windows.Forms.Padding(2);
            this.Listen_Btn.Name = "Listen_Btn";
            this.Listen_Btn.Size = new System.Drawing.Size(100, 46);
            this.Listen_Btn.TabIndex = 1;
            this.Listen_Btn.Text = "Listen";
            this.Listen_Btn.UseVisualStyleBackColor = true;
            this.Listen_Btn.Click += new System.EventHandler(this.Listen_Btn_Click);
            // 
            // listMessages
            // 
            this.listMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listMessages.FormattingEnabled = true;
            this.listMessages.ItemHeight = 26;
            this.listMessages.Location = new System.Drawing.Point(45, 110);
            this.listMessages.Margin = new System.Windows.Forms.Padding(2);
            this.listMessages.MultiColumn = true;
            this.listMessages.Name = "listMessages";
            this.listMessages.ScrollAlwaysVisible = true;
            this.listMessages.Size = new System.Drawing.Size(524, 212);
            this.listMessages.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Port";
            // 
            // Server_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listMessages);
            this.Controls.Add(this.Listen_Btn);
            this.Controls.Add(this.txtPort);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Server_1";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button Listen_Btn;
        private System.Windows.Forms.ListBox listMessages;
        private System.Windows.Forms.Label label1;
    }
}

