namespace Server
{
    partial class Server
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PortLabel = new Label();
            PortTextBox = new TextBox();
            ServerStartButton = new Button();
            ServerStopButton = new Button();
            MessageListBox = new ListBox();
            IPLabel = new Label();
            ConnectedGroupBox = new GroupBox();
            ConnectedListBox = new ListBox();
            GameFinishButton = new Button();
            GameStartButton = new Button();
            TimeLimitSetting = new NumericUpDown();
            TimeLimitLabel = new Label();
            TimeLimitSetButton = new Button();
            ConnectedGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TimeLimitSetting).BeginInit();
            SuspendLayout();
            // 
            // PortLabel
            // 
            PortLabel.AutoSize = true;
            PortLabel.Location = new Point(323, 11);
            PortLabel.Margin = new Padding(2, 0, 2, 0);
            PortLabel.Name = "PortLabel";
            PortLabel.Size = new Size(48, 25);
            PortLabel.TabIndex = 0;
            PortLabel.Text = "포트";
            // 
            // PortTextBox
            // 
            PortTextBox.Location = new Point(323, 38);
            PortTextBox.Margin = new Padding(2);
            PortTextBox.Name = "PortTextBox";
            PortTextBox.Size = new Size(155, 31);
            PortTextBox.TabIndex = 1;
            PortTextBox.KeyDown += PortTextBox_KeyDown;
            // 
            // ServerStartButton
            // 
            ServerStartButton.Location = new Point(482, 35);
            ServerStartButton.Margin = new Padding(2);
            ServerStartButton.Name = "ServerStartButton";
            ServerStartButton.Size = new Size(154, 36);
            ServerStartButton.TabIndex = 2;
            ServerStartButton.Text = "서버 시작";
            ServerStartButton.UseVisualStyleBackColor = true;
            ServerStartButton.Click += ServerStartButton_Click;
            // 
            // ServerStopButton
            // 
            ServerStopButton.Location = new Point(482, 76);
            ServerStopButton.Margin = new Padding(2);
            ServerStopButton.Name = "ServerStopButton";
            ServerStopButton.Size = new Size(154, 36);
            ServerStopButton.TabIndex = 3;
            ServerStopButton.Text = "서버 종료";
            ServerStopButton.UseVisualStyleBackColor = true;
            ServerStopButton.Click += ServerStopButton_Click;
            // 
            // MessageListBox
            // 
            MessageListBox.FormattingEnabled = true;
            MessageListBox.ItemHeight = 25;
            MessageListBox.Location = new Point(11, 11);
            MessageListBox.Margin = new Padding(2);
            MessageListBox.Name = "MessageListBox";
            MessageListBox.Size = new Size(308, 479);
            MessageListBox.TabIndex = 4;
            // 
            // IPLabel
            // 
            IPLabel.AutoSize = true;
            IPLabel.Location = new Point(323, 81);
            IPLabel.Margin = new Padding(2, 0, 2, 0);
            IPLabel.Name = "IPLabel";
            IPLabel.Size = new Size(109, 100);
            IPLabel.TabIndex = 7;
            IPLabel.Text = "로컬 IP주소:\r\n0.0.0.0\r\n외부 IP주소:\r\n0.0.0.0";
            // 
            // ConnectedGroupBox
            // 
            ConnectedGroupBox.Controls.Add(ConnectedListBox);
            ConnectedGroupBox.Location = new Point(328, 241);
            ConnectedGroupBox.Margin = new Padding(2);
            ConnectedGroupBox.Name = "ConnectedGroupBox";
            ConnectedGroupBox.Padding = new Padding(2);
            ConnectedGroupBox.Size = new Size(308, 249);
            ConnectedGroupBox.TabIndex = 8;
            ConnectedGroupBox.TabStop = false;
            ConnectedGroupBox.Text = "접속자";
            // 
            // ConnectedListBox
            // 
            ConnectedListBox.FormattingEnabled = true;
            ConnectedListBox.ItemHeight = 25;
            ConnectedListBox.Location = new Point(5, 29);
            ConnectedListBox.Margin = new Padding(2);
            ConnectedListBox.Name = "ConnectedListBox";
            ConnectedListBox.Size = new Size(299, 204);
            ConnectedListBox.TabIndex = 0;
            // 
            // GameFinishButton
            // 
            GameFinishButton.Location = new Point(482, 156);
            GameFinishButton.Margin = new Padding(2);
            GameFinishButton.Name = "GameFinishButton";
            GameFinishButton.Size = new Size(154, 36);
            GameFinishButton.TabIndex = 9;
            GameFinishButton.Text = "게임 종료";
            GameFinishButton.UseVisualStyleBackColor = true;
            GameFinishButton.Click += GameFinishButton_Click;
            // 
            // GameStartButton
            // 
            GameStartButton.Location = new Point(482, 116);
            GameStartButton.Margin = new Padding(2);
            GameStartButton.Name = "GameStartButton";
            GameStartButton.Size = new Size(154, 36);
            GameStartButton.TabIndex = 10;
            GameStartButton.Text = "게임 시작";
            GameStartButton.UseVisualStyleBackColor = true;
            GameStartButton.Click += GameStartButton_Click;
            // 
            // TimeLimitSetting
            // 
            TimeLimitSetting.Location = new Point(418, 205);
            TimeLimitSetting.Name = "TimeLimitSetting";
            TimeLimitSetting.Size = new Size(60, 31);
            TimeLimitSetting.TabIndex = 11;
            TimeLimitSetting.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // TimeLimitLabel
            // 
            TimeLimitLabel.AutoSize = true;
            TimeLimitLabel.Location = new Point(323, 207);
            TimeLimitLabel.Margin = new Padding(2, 0, 2, 0);
            TimeLimitLabel.Name = "TimeLimitLabel";
            TimeLimitLabel.Size = new Size(90, 25);
            TimeLimitLabel.TabIndex = 12;
            TimeLimitLabel.Text = "시간 제한";
            // 
            // TimeLimitSetButton
            // 
            TimeLimitSetButton.Location = new Point(483, 201);
            TimeLimitSetButton.Margin = new Padding(2);
            TimeLimitSetButton.Name = "TimeLimitSetButton";
            TimeLimitSetButton.Size = new Size(154, 36);
            TimeLimitSetButton.TabIndex = 13;
            TimeLimitSetButton.Text = "시간 제한 설정";
            TimeLimitSetButton.UseVisualStyleBackColor = true;
            TimeLimitSetButton.Click += TimeLimitSetButton_Click;
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(647, 501);
            Controls.Add(TimeLimitSetButton);
            Controls.Add(TimeLimitLabel);
            Controls.Add(TimeLimitSetting);
            Controls.Add(GameStartButton);
            Controls.Add(MessageListBox);
            Controls.Add(GameFinishButton);
            Controls.Add(ConnectedGroupBox);
            Controls.Add(IPLabel);
            Controls.Add(ServerStopButton);
            Controls.Add(ServerStartButton);
            Controls.Add(PortTextBox);
            Controls.Add(PortLabel);
            KeyPreview = true;
            Margin = new Padding(2);
            Name = "Server";
            Text = "Server";
            FormClosing += Server_FormClosing;
            ConnectedGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TimeLimitSetting).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label PortLabel;
        private TextBox PortTextBox;
        private Button ServerStartButton;
        private Button ServerStopButton;
        private ListBox MessageListBox;
        private Label IPLabel;
        private GroupBox ConnectedGroupBox;
        private ListBox ConnectedListBox;
        private Button GameFinishButton;
        private Button GameStartButton;
        private NumericUpDown TimeLimitSetting;
        private Label TimeLimitLabel;
        private Button TimeLimitSetButton;
    }
}
