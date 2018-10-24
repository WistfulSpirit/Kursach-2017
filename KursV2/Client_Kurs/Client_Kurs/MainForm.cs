using System;
using System.Windows.Forms;

namespace Client_Kurs
{
    public partial class MainForm : Form
    {

        Client Nclient;

        public MainForm()
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

        private void SendButton_Click(object sender, EventArgs e)
        {
            Nclient.SendMessage(UsrNmTXB.Text + ": " + Message.Text);
            Message.Clear();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Nclient?.SendMessage("#1md0n3");
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            string IPdef = Properties.Settings.Default.ServersIPStatic.ToString();
            int Portdef = Properties.Settings.Default.ServersPortStatic;
            ConnectButton.Enabled = false;
            Nclient = new Client(IPdef, Portdef, UsrNmTXB.Text, maskedTextBox1.Text);
            //Nclient.ConnectToServer(ConnectButton, SendButton);
            Nclient.ConnectToServer(ConnectButton, SendButton, Message, UsrNmTXB, maskedTextBox1);
            Nclient.ReadStream(ChatBox, ConnectButton, SendButton, Message, UsrNmTXB, maskedTextBox1);
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.Owner = this;
            if (settings.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Настройки успешно сохранены", "Успех");
            }
        }

        private void Message_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar))
                e.Handled = false;
            if ((sender as RichTextBox).Text.Length >= 400)
                e.Handled = true;
        }

        private void Message_TextChanged(object sender, EventArgs e)
        {
            if (!ConnectButton.Enabled)
            {
                label2.Text = Message.Text.Length + "/400 символов";
                if (Message.Text.Length > 400 || String.IsNullOrWhiteSpace(Message.Text))
                {
                    label2.ForeColor = System.Drawing.Color.Red;
                    SendButton.Enabled = false;
                }
                else
                {
                    label2.ForeColor = System.Drawing.Color.Black;
                    SendButton.Enabled = true;
                }

            }
            else
                Message.Enabled = false;
        }

        private void maskedTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!SendButton.Enabled)
            {
                if (String.IsNullOrWhiteSpace(UsrNmTXB.Text) || String.IsNullOrWhiteSpace(maskedTextBox1.Text))
                    ConnectButton.Enabled = false;
                else
                    ConnectButton.Enabled = true;
            }
        }
    }
}

