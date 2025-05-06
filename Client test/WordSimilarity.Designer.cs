namespace Client
{
    partial class WordSimilarity
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordSimilarity));
            GivenWord = new Label();
            WordInput = new TextBox();
            Score = new Label();
            Submit = new Button();
            Timer = new System.Windows.Forms.Timer(components);
            TimeProgressBar = new ProgressBar();
            TimeLabel = new Label();
            SuspendLayout();
            // 
            // GivenWord
            // 
            GivenWord.AutoSize = true;
            GivenWord.Font = new Font("맑은 고딕", 12F);
            GivenWord.Location = new Point(12, 9);
            GivenWord.Name = "GivenWord";
            GivenWord.Size = new Size(78, 32);
            GivenWord.TabIndex = 0;
            GivenWord.Text = "label1";
            // 
            // WordInput
            // 
            WordInput.Font = new Font("맑은 고딕", 16F);
            WordInput.Location = new Point(12, 70);
            WordInput.Name = "WordInput";
            WordInput.Size = new Size(312, 50);
            WordInput.TabIndex = 1;
            // 
            // Score
            // 
            Score.AutoSize = true;
            Score.Font = new Font("맑은 고딕", 12F);
            Score.Location = new Point(12, 144);
            Score.Name = "Score";
            Score.Size = new Size(78, 32);
            Score.TabIndex = 2;
            Score.Text = "label2";
            // 
            // Submit
            // 
            Submit.Font = new Font("맑은 고딕", 10F);
            Submit.Location = new Point(340, 70);
            Submit.Name = "Submit";
            Submit.Size = new Size(112, 50);
            Submit.TabIndex = 3;
            Submit.Text = "제출";
            Submit.UseVisualStyleBackColor = true;
            Submit.Click += Submit_Click;
            // 
            // Timer
            // 
            Timer.Enabled = true;
            Timer.Interval = 1000;
            Timer.Tick += Timer_Tick;
            // 
            // TimeProgressBar
            // 
            TimeProgressBar.Location = new Point(12, 277);
            TimeProgressBar.Name = "TimeProgressBar";
            TimeProgressBar.Size = new Size(436, 19);
            TimeProgressBar.TabIndex = 4;
            // 
            // TimeLabel
            // 
            TimeLabel.AutoSize = true;
            TimeLabel.Font = new Font("맑은 고딕", 12F);
            TimeLabel.Location = new Point(12, 242);
            TimeLabel.Name = "TimeLabel";
            TimeLabel.Size = new Size(72, 32);
            TimeLabel.TabIndex = 5;
            TimeLabel.Text = "Time:";
            // 
            // WordSimilarity
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 308);
            Controls.Add(TimeLabel);
            Controls.Add(TimeProgressBar);
            Controls.Add(Submit);
            Controls.Add(Score);
            Controls.Add(WordInput);
            Controls.Add(GivenWord);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "WordSimilarity";
            Text = "Word Similarity";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label GivenWord;
        private TextBox WordInput;
        private Label Score;
        private Button Submit;
        private System.Windows.Forms.Timer Timer;
        private ProgressBar TimeProgressBar;
        private Label TimeLabel;
    }
}