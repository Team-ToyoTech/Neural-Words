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

namespace WordSimilarity
{
    public partial class WordSimilarity : Form
    {
        private FastTextWrapper fastText;
        private string filePath = "D:\\Machine Learning\\cc.ko.300.bin"; // 모델 경로

        public WordSimilarity()
        {
            InitializeComponent();
            fastText = new FastTextWrapper();
            fastText.LoadModel(filePath);

            // TODO: 서버에서 단어 가져오기
            // string path = @"D:\\Machine Learning\\wordList-utf8.txt";
            // string randomWord = RandomWordSelector.GetRandomWord(path);
            // GivenWord.Text = randomWord; // 초기 단어 설정
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

            // (도쿄 - 일본 + 한국)과 서울 벡터 비교 -> 56.8%
            // var vector1 = fastText.GetWordVector("도쿄");
            // var vector2 = fastText.GetWordVector("일본");
            // var vector3 = fastText.GetWordVector("한국");
            // var givenVector = fastText.GetWordVector("서울");
            // var vector = fastText.GetWordVector("서울");
            // for (int i = 0; i < vector.Length; i++)
            // {
            //     givenVector[i] = vector1[i] - vector2[i] + vector3[i]; // 벡터 연산
            // }

            double norm = 0, givenNorm = 0;
            double dot = 0;
            string score = "";
            for (int i = 0; i < vector.Length; i++)
            {
                dot += vector[i] * givenVector[i];
                norm += vector[i] * vector[i];
                givenNorm += givenVector[i] * givenVector[i];
            }
            score = (dot / Math.Sqrt(norm * givenNorm) * 100).ToString("F1") + "%"; // 점수 계산
            Score.Text = score; // 점수 출력

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

            // TODO: 서버로 점수 보내기 구현
        }
    }
}
