using FastText.NetWrapper;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public partial class Client : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;
        private WordSimilarity wordSimilarity;
        int mynum;
        bool isconnected;
        string nickname;
        static string str;

        internal FastTextWrapper fastText;
        internal string filePath = @"D:\Source\Repos\Neural-Words\Client test\cc.ko.300.bin"; // FastText 모델 경로

        public Client()
        {
            InitializeComponent();
            fastText = new FastTextWrapper();
            fastText.LoadModel(filePath);
            DisconnectButton.Enabled = false;
            isconnected = false;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(PortTextBox.Text, out int port))
                {
                    client = new TcpClient(IPTextBox.Text, port);
                    stream = client.GetStream();
                    receiveThread = new Thread(ReceiveMessages);
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                    MessageListBox.Items.Add("Connected to server");
                    isconnected = true;
                    DisconnectButton.Enabled = true;
                    ConnectButton.Enabled = false;
                    NicknameTextBox.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReceiveMessages()
        {
            byte[] buffer = new byte[102400];
            string msg = "";

            while (true)
            {
                try
                {
                    buffer = new byte[102400];
                    if (msg != "")
                    {
                        buffer = Encoding.UTF8.GetBytes(msg);
                    }
                    while (true)
                    {
                        byte[] data = new byte[256];
                        int bytesRead = stream.Read(data, 0, data.Length);
                        if (bytesRead == 0)
                            break;
                        data = data.Where(x => x != 0).ToArray();
                        if (buffer.Length == 102400) buffer = data;
                        else buffer = buffer.Concat(data).ToArray();

                        msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        if (msg.Contains('◊')) break;
                    }
                    if (Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split("◊").Length == 1)
                        msg = "";
                    else msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split("◊")[1];
                    string[] message = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split("◊")[0].Split('⧫');

                    if (message[0] == "1") // 연결 종료
                    {

                        client.Close();
                        if (message[1] != "")
                            MessageBox.Show(message[1]);

                        Invoke(new Action(() =>
                        {
                            MessageListBox.Items.Add("Disconnected from server");
                            DisconnectButton.Enabled = false;
                            ConnectButton.Enabled = true;
                            isconnected = false;
                            NicknameTextBox.Enabled = true;
                            ConnectedListBox.Items.Clear();
                        }));

                        break;
                    }
                    else if (message[0] == "2") // 번호 지정
                    {
                        mynum = int.Parse(message[1]);
                        Invoke(new Action(() => str = NicknameTextBox.Text));
                        if (str == "")
                        {
                            nickname = "Client" + mynum.ToString();
                            Invoke(new Action(() => NicknameTextBox.Text = nickname));
                        }
                        else if (!str.Contains('⧫'))
                        {
                            nickname = str;
                        }
                        else
                        {
                            MessageBox.Show("이름에 다음 문자가 포함되어서는 안됩니다: ⧫\n기본 이름으로 진행합니다.");
                            nickname = "Client" + mynum.ToString();
                            Invoke(new Action(() => NicknameTextBox.Text = nickname));
                        }
                        stream.Write(Encoding.UTF8.GetBytes("3⧫" + nickname + '◊'));
                        stream.Flush();
                    }
                    else if (message[0] == "4") // 접속 클라이언트 이름
                    {
                        Invoke(new Action(() => ConnectedListBox.Items.Add(message[1])));
                        Invoke(new Action(() => MessageListBox.Items.Add(message[1] + " connected")));
                    }
                    else if (message[0] == "5") // 접속 종료 클라이언트 이름
                    {
                        Invoke(new Action(() => ConnectedListBox.Items.Remove(message[1])));
                        Invoke(new Action(() => MessageListBox.Items.Add(message[1] + " disconnected")));
                    }
                    else if (message[0] == "6") // 게임 시작
                    {
                        //Invoke(new Action(() => listBox1.Items.Add(message[1])));
                        //wordSimilarity = new WordSimilarity(this);
                        //wordSimilarity.OnMessageSent += HandleMessage;
                        //wordSimilarity.Show();

                        Invoke(new Action(() =>
                        {
                            try
                            {
                                MessageListBox.Items.Add(message[1]);
                                wordSimilarity = new WordSimilarity(this);
                                wordSimilarity.OnMessageSent += HandleMessage;
                                wordSimilarity.Show();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"게임 시작 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (wordSimilarity != null)
                                {
                                    wordSimilarity.Dispose();
                                    wordSimilarity = null;
                                }
                            }
                        }));
                    }
                    else if (message[0] == "7") // 게임 종료
                    {
                        Invoke(new Action(() => MessageListBox.Items.Add(message[1])));
                        wordSimilarity.Close();
                        wordSimilarity.Dispose();
                    }
                    else if (message[0] == "8") // 단어 받기
                    {
                        Invoke(new Action(() => MessageListBox.Items.Add("Received Word: " + message[1])));
                        wordSimilarity.ReceiveMessage(message[1]);
                    }

                    Invoke(new Action(() => MessageListBox.TopIndex = MessageListBox.Items.Count - 1));
                }
                catch (Exception ex)
                {
                    break;
                }
            }
        }

        private void HandleMessage(string message) // word similarity 점수 메시지 받아서 server로 전송
        {
            stream.Write(Encoding.UTF8.GetBytes("9⧫" + message + "◊"));
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            stream.Write(Encoding.UTF8.GetBytes("1⧫◊"));
            stream.Flush();
            stream.Close();
            client.Close();
            MessageListBox.Items.Add("Disconnected from server");
            DisconnectButton.Enabled = false;
            ConnectButton.Enabled = true;
            isconnected = false;
            NicknameTextBox.Enabled = true;
            ConnectedListBox.Items.Clear();
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isconnected)
            {
                stream.Write(Encoding.UTF8.GetBytes("1⧫◊"));
                stream.Flush();
                stream.Close();
                client.Close();
                MessageListBox.Items.Add("Disconnected from server");
                DisconnectButton.Enabled = false;
                ConnectButton.Enabled = true;
                isconnected = false;
                NicknameTextBox.Enabled = true;
            }
        }

        private void NicknameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectButton.PerformClick();
            }
        }
    }
}
