using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastText.NetWrapper;
using static System.Formats.Asn1.AsnWriter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class WordSimilarity : Form
    {
        public event Action<string> OnMessageSent; // client로 메시지 전송

        private int remainingTime;  // 남은 시간(초)
        private int totalTime = 15; // 총 시간(초)

        Client client;

        public WordSimilarity(Client client)
        {
            InitializeComponent();
            TimeProgressBar.Maximum = totalTime;
            TimeProgressBar.Value = totalTime;
            this.client = client;
        }

        private void WordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Submit.PerformClick(); // Enter 키를 누르면 Submit 버튼 클릭
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if (WordInput.Text == "" || WordInput.Text == GivenWord.Text)
            {
                MessageBox.Show("단어가 올바르지 않습니다.");
                return;
            }

            Timer.Stop(); // 타이머 정지

            var vector = client.fastText.GetWordVector(WordInput.Text);
            var givenVector = client.fastText.GetWordVector(GivenWord.Text);

            double norm = 0, givenNorm = 0;
            double dot = 0;
            double score = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                dot += vector[i] * givenVector[i];
                norm += vector[i] * vector[i];
                givenNorm += givenVector[i] * givenVector[i];
            }
            score = (dot / Math.Sqrt(norm * givenNorm) + 1) / 2 * 100; // 점수 계산
            Score.Text = score.ToString("F1") + "%"; // 점수 출력

            string message = score.ToString("F1");
            OnMessageSent?.Invoke(message); // Client로 점수 전송

            // 벡터 내용 확인 (모두 출력)
            // label1.Text = vector.Length.ToString() + " " + givenVector.Length.ToString() + "\n";
            // foreach (var i in vector)
            // {
            //     label1.Text += i.ToString() + " ";
            // }
            // label1.Text += "\n";
            // foreach (var i in givenVector)
            // {
            //     label1.Text += i.ToString() + " ";
            // }
        }

        public void ReceiveMessage(string message) // Client에서 단어 받기
        {
            GivenWord.Text = message; // 단어 표시
            remainingTime = totalTime;
            TimeProgressBar.Value = totalTime;
            remainingTime = totalTime; // 남은 시간 초기화
            Timer.Start();
        }

        public void ReceiveTimeLimit(string message)
        {
            totalTime = Int32.Parse(message);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime--;

            if (remainingTime >= 0)
            {
                TimeProgressBar.Value = remainingTime;
                TimeLabel.Text = "남은 시간: " + remainingTime.ToString() + "초";
            }
            else
            {
                Timer.Stop();
                MessageBox.Show("시간 종료");
            }
        }
    }
}
