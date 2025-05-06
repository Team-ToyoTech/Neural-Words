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
            timer1 = new System.Windows.Forms.Timer(components);
            progressBar1 = new ProgressBar();
            label1 = new Label();
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
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 277);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(436, 19);
            progressBar1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 12F);
            label1.Location = new Point(12, 242);
            label1.Name = "label1";
            label1.Size = new Size(72, 32);
            label1.TabIndex = 5;
            label1.Text = "Time:";
            // 
            // WordSimilarity
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 308);
            Controls.Add(label1);
            Controls.Add(progressBar1);
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
        private System.Windows.Forms.Timer timer1;
        private ProgressBar progressBar1;
        private Label label1;
    }
}