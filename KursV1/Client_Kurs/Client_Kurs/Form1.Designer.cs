namespace Client_Kurs
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Message = new System.Windows.Forms.RichTextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.ChatBox = new System.Windows.Forms.RichTextBox();
            this.IP = new System.Windows.Forms.TextBox();
            this.PortText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.UsrNmTXB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Message
            // 
            this.Message.Location = new System.Drawing.Point(12, 280);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(434, 91);
            this.Message.TabIndex = 0;
            this.Message.Text = "";
            // 
            // SendButton
            // 
            this.SendButton.Enabled = false;
            this.SendButton.Location = new System.Drawing.Point(469, 280);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(79, 91);
            this.SendButton.TabIndex = 1;
            this.SendButton.Text = "Отправить";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChatBox
            // 
            this.ChatBox.Location = new System.Drawing.Point(12, 84);
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.Size = new System.Drawing.Size(536, 181);
            this.ChatBox.TabIndex = 3;
            this.ChatBox.Text = "";
            // 
            // IP
            // 
            this.IP.Location = new System.Drawing.Point(12, 35);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(99, 20);
            this.IP.TabIndex = 6;
            this.IP.Text = "192.168.100.2";
            // 
            // PortText
            // 
            this.PortText.Location = new System.Drawing.Point(141, 35);
            this.PortText.Name = "PortText";
            this.PortText.Size = new System.Drawing.Size(78, 20);
            this.PortText.TabIndex = 7;
            this.PortText.Text = "5354";
            this.PortText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(340, 374);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(359, 374);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "/500 символов";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "IP server";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(138, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Port";
            // 
            // UsrNmTXB
            // 
            this.UsrNmTXB.Location = new System.Drawing.Point(259, 35);
            this.UsrNmTXB.Name = "UsrNmTXB";
            this.UsrNmTXB.Size = new System.Drawing.Size(169, 20);
            this.UsrNmTXB.TabIndex = 7;
            this.UsrNmTXB.Text = "newuser";
            this.UsrNmTXB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(256, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Сообщения";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(469, 34);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(79, 20);
            this.ConnectButton.TabIndex = 11;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 394);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UsrNmTXB);
            this.Controls.Add(this.PortText);
            this.Controls.Add(this.IP);
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.Message);
            this.Name = "Form1";
            this.Text = "LS chat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Message;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.RichTextBox ChatBox;
        private System.Windows.Forms.TextBox IP;
        private System.Windows.Forms.TextBox PortText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox UsrNmTXB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button ConnectButton;
    }
}

