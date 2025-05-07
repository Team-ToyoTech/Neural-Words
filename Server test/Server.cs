using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;

namespace Server
{
    public partial class Server : Form
    {
        static TcpListener server;
        static List<Client> clients;
        Thread T;
        List<Thread> Tt;
        static bool isServerRun;
        static bool isClosing;
        double[] clientScore;
        int receivedClientCnt = 0;

        public Server()
        {
            InitializeComponent();
            clients = new List<Client>();
            isServerRun = false;
            T = new Thread(() => ServerLoop(1111));
            Tt = new List<Thread>();
            ServerStopButton.Enabled = false;
            GameStartButton.Enabled = false;
            GameFinishButton.Enabled = false;
            TimeLimitSetting.Enabled = false;
            TimeLimitSetButton.Enabled = false;
            isClosing = false;
            IPLabel.Text = "로컬 IP주소:\n" + GetLocalIPAddress() + "\n외부 IP주소:\n" + GetExternalIPAddress();
        }

        private void ServerStartButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(PortTextBox.Text, out int port) && 0 < port && port < 100000)
            {

                T = new Thread(() => ServerLoop(port));
                T.IsBackground = true;
                T.Start();
                ServerStartButton.Enabled = false;
                ServerStopButton.Enabled = true;
                GameStartButton.Enabled = true;
                GameFinishButton.Enabled = false;
                isServerRun = true;
                MessageListBox.Items.Add("Server started");
            }
            else
            {
                MessageBox.Show("포트는 1에서 99999 사이의 정수를 입력해 주세요");
            }
        }

        /*
        입력 코드
        1: 연결종료
        2: 번호 지정(서버=>클라이언트)
        3: 닉네임 전송(클라이언트=>서버)
        4: 접속한 클라이언트 이름
        5: 접속 종료한 클라이언트 이름
        6: 게임 시작
        7: 게임 종료
        8: 단어 전송
        9: 단어 점수 전송
        10: 시간 제한 전송
         */
        // Split 문자 : ⧫
        // 송신 Check 문자 : ◊

        public void Delay(int ms)
        {
            DateTime dateTimeNow = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime dateTimeAdd = dateTimeNow.Add(duration);
            while (dateTimeAdd >= dateTimeNow)
            {
                System.Windows.Forms.Application.DoEvents();
                dateTimeNow = DateTime.Now;
            }
            return;
        }

        // Thread function
        void ServerLoop(int port)
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            isServerRun = true;

            int count = 0;

            while (true)
            {
                try
                {
                    clients.Add(new Client(server.AcceptTcpClient(), count));
                    Invoke(new Action(() => ConnectedListBox.Items.Add(clients[clients.Count - 1].nickname)));
                    count++;

                    Tt.Add(new Thread(() => ClientCheck(clients.Count - 1, count)));
                    Delay(100);
                    clients[clients.Count - 1].Send("2", count.ToString());
                    // clients[clients.Count - 1].client.GetStream().Write(Encoding.UTF8.GetBytes($"2⧫{count}◊"));
                    Tt[Tt.Count - 1].IsBackground = true;
                    Tt[Tt.Count - 1].Start();
                }
                catch (Exception ex)
                {
                    break;
                }
            }
        }

        void ClientCheck(int clientrealnumber, int clientn)
        {
            Client client = clients[clientrealnumber];
            NetworkStream stream = clients[clientrealnumber].client.GetStream();
            byte[] buffer = new byte[102400];
            buffer[102399] = 255;
            bool error = false;
            string msg = "";
            while (isServerRun)
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
                        {
                            break;
                        }
                        data = data.Where(x => x != 0).ToArray();
                        if (buffer.Length == 102400) buffer = data;
                        else buffer = buffer.Concat(data).ToArray();

                        msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        if (msg.Contains('◊'))
                        {
                            break;
                        }
                    }
                    if (Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split("◊").Length == 1)
                    {
                        msg = "";
                    }
                    else
                    {
                        msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split("◊")[1];
                    }
                    string[] message = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split("◊")[0].Split('⧫');
                    if (message[0] == "0")
                    {
                        Invoke(new Action(() => MessageListBox.Items.Add(message[1])));

                        foreach (var c in clients)
                        {
                            if (c != client)
                            {
                                NetworkStream cStream = c.client.GetStream();
                                byte[] responseBytes = Encoding.UTF8.GetBytes("0⧫" + message[1] + '◊');
                                cStream.Write(responseBytes, 0, responseBytes.Length);
                            }
                        }
                    }
                    else if (message[0] == "1")
                    {
                        Invoke(new Action(() => MessageListBox.Items.Add($"{client.nickname} disconnected")));
                        Invoke(new Action(() => ConnectedListBox.Items.Remove(client.nickname)));
                        foreach (var c in clients)
                        {
                            NetworkStream cStream = c.client.GetStream();
                            byte[] responseBytes = buffer;
                            if (c != client)
                            {
                                cStream.Write(Encoding.UTF8.GetBytes($"0⧫{client.nickname} disconnected◊"));
                                cStream.Flush();
                                Delay(100);
                                cStream.Write(Encoding.UTF8.GetBytes($"5⧫{client.nickname}◊"));
                                cStream.Flush();
                            }

                        }
                        break;
                    }
                    else if (message[0] == "3")
                    {
                        foreach (var c in clients)
                        {
                            if (c.nickname == message[1])
                            {
                                string nickname = "";
                                foreach (var c2 in clients)
                                {
                                    if (c2 != client)
                                        nickname += c2.nickname + ", ";
                                }
                                client.Send("1", "닉네임은 다음과 같을 수 없습니다: " + nickname);
                                // client.client.GetStream().Write(Encoding.UTF8.GetBytes("1⧫닉네임은 다음과 같을 수 없습니다:" + nickname + '◊'));
                                clients.Remove(client);
                                Invoke(new Action(() => ConnectedListBox.Items.Remove(client.nickname)));
                                int b = 0;
                                error = true;
                                int a = 10 / b;
                            }
                        }
                        clients.Remove(client);
                        Invoke(new Action(() => ConnectedListBox.Items.Remove(client.nickname)));
                        client.nickname = message[1];
                        foreach (var c in clients)
                        {
                            client.Send("4", c.nickname);
                            // client.client.GetStream().Write(Encoding.UTF8.GetBytes("4⧫" + c.nickname + '◊'));
                            client.client.GetStream().Flush();
                            Delay(100);
                        }
                        clients.Add(client);
                        foreach (var c in clients)
                        {
                            c.Send("4", client.nickname);
                            // c.client.GetStream().Write(Encoding.UTF8.GetBytes("4⧫" + client.nickname + '◊'));
                        }
                        Invoke(new Action(() => ConnectedListBox.Items.Add(client.nickname)));
                        Invoke(new Action(() => MessageListBox.Items.Add($"{message[1]} joined")));
                        buffer = Encoding.UTF8.GetBytes($"0⧫{client.nickname} joined◊");
                        foreach (var c in clients)
                        {
                            NetworkStream s = c.client.GetStream();
                            s.Write(buffer, 0, buffer.Length);
                        }
                    }
                    else if (message[0] == "9") // 점수 전송
                    {
                        clientScore[clientrealnumber] += double.Parse(message[1]);
                        receivedClientCnt++;
                        Invoke(new Action(() => MessageListBox.Items.Add($"{client.nickname} Score: " + message[1] + $", {receivedClientCnt}")));
                        if (receivedClientCnt == clients.Count)
                        {
                            receivedClientCnt = 0;
                            string rndWrd = GetRandomWord(); // 랜덤 단어 가져오기
                            foreach (var c in clients)
                            {
                                c.Send("8", rndWrd);
                            }
                            Invoke(new Action(() => MessageListBox.Items.Add("Sended Word: " + rndWrd)));
                        }
                    }

                    Invoke(new Action(() => MessageListBox.TopIndex = MessageListBox.Items.Count - 1));
                }
                catch (Exception e)
                {
                    break;
                }
            }

            client.client.Close();
            if (!isClosing)
            {
                Invoke(new Action(() => MessageListBox.Items.Remove(client.nickname)));
                clients.Remove(client);
            }
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
            foreach (var c in clients)
            {
                NetworkStream n = c.client.GetStream();
                n.Write(Encoding.UTF8.GetBytes("1⧫◊"));
                c.client.Close();
            }
        }

        private void ServerStopButton_Click(object sender, EventArgs e)
        {
            foreach (var c in clients)
            {
                c.client.GetStream().Write(Encoding.UTF8.GetBytes("1⧫◊"));
                c.client.Close();
            }
            ServerStopButton.Enabled = false;
            ServerStartButton.Enabled = true;
            GameStartButton.Enabled = false;
            GameFinishButton.Enabled = false;
            TimeLimitSetButton.Enabled = false;
            TimeLimitSetting.Enabled = false;
            isServerRun = false;
            MessageListBox.Items.Add("Server stopped");
            server.Stop();
            ConnectedListBox.Items.Clear();
        }

        static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("로컬 IP 주소를 찾을 수 없습니다.");
        }

        static string GetExternalIPAddress()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                using (WebClient client = new WebClient())
                {
                    string externalIp = client.DownloadString("https://api.ipify.org");
                    return externalIp;
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("웹 예외 발생: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("내부 예외: " + ex.InnerException.Message);
                }
                return "오류 발생";
            }

            // using (WebClient client = new WebClient())
            // {
            //     string response = client.DownloadString("https://api.ipify.org");
            //     return response;
            // }
        }

        private void GameStartButton_Click(object sender, EventArgs e) // 게임 시작
        {
            if (clients.Count >= 2)
            {
                GameFinishButton.Enabled = true; // 게임 종료 버튼 활성화
                GameStartButton.Enabled = false; // 게임 시작 버튼 비활성화
                TimeLimitSetButton.Enabled = true;
                TimeLimitSetting.Enabled = true;
                Thread t = new Thread(Game);
                t.IsBackground = true;
                receivedClientCnt = 0;
                t.Start();
            }
            else
            {
                MessageBox.Show("최소 2명의 플레이어가 필요합니다.");
            }
        }

        void Game() // 게임 플레이
        {
            clientScore = Enumerable.Repeat<double>(0, clients.Count).ToArray<double>();

            foreach (var c in clients)
            {
                if (c.client.Connected)
                {
                    c.Send("6", "Game Started");
                    Delay(10);
                }
                else
                {
                    MessageBox.Show($"{c.nickname} 클라이언트가 연결되지 않았습니다.");
                }
            }
            Invoke(new Action(() => MessageListBox.Items.Add("Game Started")));

            string rndWrd = GetRandomWord(); // 랜덤 단어 가져오기
            foreach (var c in clients)
            {
                c.Send("8", rndWrd);
            }
            Invoke(new Action(() => MessageListBox.Items.Add("Sended Word: " + rndWrd)));
        }

        private string GetRandomWord()
        {
            string path = @"D:\Source\Repos\Neural-Words\Server test\wordList-utf8.txt";
            if (!File.Exists(path))
            {
                MessageBox.Show("단어 파일이 존재하지 않습니다. 경로를 확인하세요." + path);
                return string.Empty;
            }

            string randomWord = RandomWordSelector.GetRandomWord(path);
            return randomWord;
        }

        private void GameFinishButton_Click(object sender, EventArgs e) // 게임 종료
        {
            GameFinishButton.Enabled = false; // 게임 종료 버튼 비활성화
            GameStartButton.Enabled = true; // 게임 시작 버튼 활성화
            TimeLimitSetting.Enabled = false;
            TimeLimitSetButton.Enabled = false;
            foreach (var c in clients)
            {
                c.Send("7", "Game Ended");
                Delay(10);
            }
            Invoke(new Action(() => MessageListBox.Items.Add("Game Ended")));
        }

        private void PortTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ServerStartButton.PerformClick();
            }
        }

        private void TimeLimitSetButton_Click(object sender, EventArgs e)
        {
            foreach (var c in clients)
            {
                c.Send("10", TimeLimitSetting.Value.ToString());
            }
            Invoke(new Action(() => MessageListBox.Items.Add($"Time Limit Setted: {TimeLimitSetting.Value}s")));
        }
    }

    class Client
    {
        public TcpClient client;
        public string nickname;

        public Client(TcpClient client, int n)
        {
            this.client = client;
            nickname = "Client" + n.ToString();
        }

        public Client(TcpClient client, string str)
        {
            this.client = client;
            nickname = str;
        }

        public void Send(string type, string msg)
        {
            client.GetStream().Write(Encoding.UTF8.GetBytes(type + "⧫" + msg + "◊"));
        }
    }
}
