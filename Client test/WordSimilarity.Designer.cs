namespace Client_test
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordSimilarity));
            GivenWord = new Label();
            WordInput = new TextBox();
            Score = new Label();
            Submit = new Button();
            SuspendLayout();
            // 
            // GivenWord
            // 
            GivenWord.AutoSize = true;
            GivenWord.Location = new Point(54, 68);
            GivenWord.Name = "GivenWord";
            GivenWord.Size = new Size(60, 25);
            GivenWord.TabIndex = 0;
            GivenWord.Text = "label1";
            // 
            // WordInput
            // 
            WordInput.Location = new Point(54, 96);
            WordInput.Name = "WordInput";
            WordInput.Size = new Size(150, 31);
            WordInput.TabIndex = 1;
            // 
            // Score
            // 
            Score.AutoSize = true;
            Score.Location = new Point(54, 130);
            Score.Name = "Score";
            Score.Size = new Size(60, 25);
            Score.TabIndex = 2;
            Score.Text = "label2";
            // 
            // Submit
            // 
            Submit.Location = new Point(210, 93);
            Submit.Name = "Submit";
            Submit.Size = new Size(112, 34);
            Submit.TabIndex = 3;
            Submit.Text = "제출";
            Submit.UseVisualStyleBackColor = true;
            Submit.Click += Submit_Click;
            // 
            // WordSimilarity
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Submit);
            Controls.Add(Score);
            Controls.Add(WordInput);
            Controls.Add(GivenWord);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "WordSimilarity";
            Text = "WordSimilarity";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label GivenWord;
        private TextBox WordInput;
        private Label Score;
        private Button Submit;
    }
}