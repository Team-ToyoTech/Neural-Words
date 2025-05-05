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

namespace Client_test
{
    public partial class WordSimilarity : Form
    {
        public event Action<string> OnMessageSent; // client로 메시지 전송

        private FastTextWrapper fastText;
        private string filePath = @"D:\Source\Repos\Neural-Words\Client test\cc.ko.300.bin"; // FastText 모델 경로

        public WordSimilarity(Client Form)
        {
            InitializeComponent();
            fastText = new FastTextWrapper();
            fastText.LoadModel(filePath);
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
            var vector = fastText.GetWordVector(WordInput.Text);
            var givenVector = fastText.GetWordVector(GivenWord.Text);

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
        }
    }
}
