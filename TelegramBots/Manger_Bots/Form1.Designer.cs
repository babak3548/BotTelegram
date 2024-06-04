namespace Manger_Bots
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
            this.getUpdate = new System.Windows.Forms.Button();
            this.rtGetUpdate = new System.Windows.Forms.RichTextBox();
            this.rtSendMessage = new System.Windows.Forms.RichTextBox();
            this.bSendMessage = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.testDbConn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // getUpdate
            // 
            this.getUpdate.Location = new System.Drawing.Point(805, 214);
            this.getUpdate.Name = "getUpdate";
            this.getUpdate.Size = new System.Drawing.Size(75, 23);
            this.getUpdate.TabIndex = 0;
            this.getUpdate.Text = "getUpdate";
            this.getUpdate.UseVisualStyleBackColor = true;
            this.getUpdate.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtGetUpdate
            // 
            this.rtGetUpdate.Location = new System.Drawing.Point(33, 13);
            this.rtGetUpdate.Name = "rtGetUpdate";
            this.rtGetUpdate.Size = new System.Drawing.Size(847, 195);
            this.rtGetUpdate.TabIndex = 2;
            this.rtGetUpdate.Text = "";
            // 
            // rtSendMessage
            // 
            this.rtSendMessage.Location = new System.Drawing.Point(33, 243);
            this.rtSendMessage.Name = "rtSendMessage";
            this.rtSendMessage.Size = new System.Drawing.Size(847, 65);
            this.rtSendMessage.TabIndex = 3;
            this.rtSendMessage.Text = "";
            // 
            // bSendMessage
            // 
            this.bSendMessage.Location = new System.Drawing.Point(783, 314);
            this.bSendMessage.Name = "bSendMessage";
            this.bSendMessage.Size = new System.Drawing.Size(97, 23);
            this.bSendMessage.TabIndex = 4;
            this.bSendMessage.Text = "getRequestsTest";
            this.bSendMessage.UseVisualStyleBackColor = true;
            this.bSendMessage.Click += new System.EventHandler(this.bSendMessage_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(690, 314);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // testDbConn
            // 
            this.testDbConn.Location = new System.Drawing.Point(604, 314);
            this.testDbConn.Name = "testDbConn";
            this.testDbConn.Size = new System.Drawing.Size(75, 23);
            this.testDbConn.TabIndex = 6;
            this.testDbConn.Text = "testDbConn";
            this.testDbConn.UseVisualStyleBackColor = true;
            this.testDbConn.Click += new System.EventHandler(this.testDbConn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(523, 314);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 369);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.testDbConn);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.bSendMessage);
            this.Controls.Add(this.rtSendMessage);
            this.Controls.Add(this.rtGetUpdate);
            this.Controls.Add(this.getUpdate);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button getUpdate;
        private System.Windows.Forms.RichTextBox rtGetUpdate;
        private System.Windows.Forms.RichTextBox rtSendMessage;
        private System.Windows.Forms.Button bSendMessage;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button testDbConn;
        private System.Windows.Forms.Button button1;
    }
}

