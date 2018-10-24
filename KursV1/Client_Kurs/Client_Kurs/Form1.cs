using System;
using System.Windows.Forms;

namespace Client_Kurs
{
    public partial class Form1 : Form
    {

        Client Nclient;

        public Form1()
        {
            InitializeComponent();
        }

       /* protected void Receiver()
        {

            //Создаем Listener на порт "по умолчанию"
            TcpListener Listen = new TcpListener(IPAddress.Any,port);
            //Начинаем прослушку
            Listen.Start();
            //и заведем заранее сокет
            Socket ReceiveSocket;
            while (true)
            {
                try
                {
                    //Пришло сообщение
                    ReceiveSocket = Listen.AcceptSocket();
                    Byte[] Receive = new Byte[1024];
                    //Читать сообщение будем в поток
                    using (MemoryStream MessageR = new MemoryStream())
                    {
                        //Количество считанных байт
                        Int32 ReceivedBytes;
                        do
                        {//Собственно читаем
                            ReceivedBytes = ReceiveSocket.Receive(Receive, Receive.Length, 0);
                            //и записываем в поток
                            MessageR.Write(Receive, 0, ReceivedBytes);
                            //Читаем до тех пор, пока в очереди не останется данных
                        } while (ReceiveSocket.Available > 0);
                        //Добавляем изменения в ChatBox
                        ChatBox.BeginInvoke(AcceptDelegate, new object[] { "Received " + Encoding.Default.GetString(MessageR.ToArray()), ChatBox });
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        void ThreadSend(object Message)
        {
            try
            {
                //Проверяем входной объект на соответствие строке
                String MessageText = "";
                if (Message is String)
                    MessageText = Message as String;
                else
                    throw new Exception("На вход необходимо подавать строку");
                //Создаем сокет, коннектимся
                IPEndPoint EndPoint = new IPEndPoint(IPAddress.Parse(IP.Text), port);
                Socket Connector = new Socket(EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Connector.Connect(EndPoint);
                //Отправляем сообщение
                Byte[] SendBytes = Encoding.Default.GetBytes(MessageText);
                Connector.Send(SendBytes);
                Connector.Close();
                //Изменяем поле сообщений (уведомляем, что отправили сообщение)
                ChatBox.BeginInvoke(AcceptDelegate, new object[] { "Send " + MessageText, ChatBox });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Делегат доступа к контролам формы
        delegate void SendMsg(String Text, RichTextBox Rtb);

        SendMsg AcceptDelegate = (String Text, RichTextBox Rtb) =>
           {
               Rtb.Text += Text + "\\";
           };*/
        private void button1_Click(object sender, EventArgs e)
        {
            Nclient.SendMessage(UsrNmTXB.Text+": "+ Message.Text);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Nclient?.SendMessage("#1md0n3");
        }



        private void ConnectButton_Click(object sender, EventArgs e)
        {
            ConnectButton.Enabled = false;
            Nclient = new Client(IP.Text, Convert.ToInt32(PortText.Text), UsrNmTXB.Text);
            Nclient.ConnectToServer(ConnectButton, SendButton);
            Nclient.ReadStream(ChatBox, ConnectButton,SendButton );
            /*ConnectButton.Enabled = false;
             DisconnectButton.Enabled = true;*/
        }

        private void ConnectButton_EnabledChanged(object sender, EventArgs e)
        {

        }
    }
}
/* Схема работы:
 * Есть приложение сервер на одном пк, а на всесх остальных приложение клиентж
 * Клиенты могут передавать сообщение друг другу без помощи сервера */
