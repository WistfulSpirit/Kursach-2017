using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client_Kurs
{

    /* Служит для:
     * Создания/инициализации нового пользователя в сети 
     * Отправки сообщейний одному-всем клиентам;
     *
     */
    class Client
    {
        TcpClient _client;
        NetworkStream netStream;

        string _ServerIP;
        public string ServIP
        {
            get { return _ServerIP; }
            set { _ServerIP = value; }
        }

        string _CurrentIP;
        public string CurIP
        {
            get { return _CurrentIP; }
            set { _CurrentIP = value; }
        }
        int _Port;
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public Client(string _IPserver, int _PortServer, string _UserName)
        {
            _ServerIP = _IPserver;
            _Port = _PortServer;
            _Name = _UserName;
        }

        public void ConnectToServer(Button conbutton, Button sendbutton)
        {
            _client = new TcpClient();
            Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        await _client.ConnectAsync(IPAddress.Parse(_ServerIP), _Port);
                        IPHostEntry thisIP = Dns.GetHostEntry(Dns.GetHostName());
                        foreach (var ip in thisIP.AddressList)
                            if (ip.AddressFamily == AddressFamily.InterNetwork)
                                _CurrentIP = ip.ToString();
                        SendMessage("#N4m3: " + _Name);
                        if (_client.Connected)
                        {
                            conbutton.BeginInvoke(new Action<Button>((btn) => btn.Enabled = false), conbutton);
                            sendbutton.BeginInvoke(new Action<Button>((btn) => btn.Enabled = true), sendbutton);
                        }
                    }
                    catch (Exception ex)
                    {
                        conbutton.BeginInvoke(new Action<Button>((btn) => btn.Enabled = true), conbutton);
                        sendbutton.BeginInvoke(new Action<Button>((btn) => btn.Enabled = false), sendbutton);
                        MessageBox.Show(ex.ToString());

                    }
                }
                );
        }

        public void ReadStream(RichTextBox rtb, Button conbutton, Button sendbutton)
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        if (_client != null && _client.Connected == true)//client !=null && client.Connected = true
                        {
                            NetworkStream stream = _client.GetStream();
                            byte[] Data = new byte[1024];
                            int bytes = await stream.ReadAsync(Data, 0, Data.Length);
                            string ms = Encoding.UTF8.GetString(Data, 0, bytes);
                            if (ms != "" && ms != "#c43k1n9")
                            {
                                if (ms.Contains("#13uRnu37:"))
                                {
                                    ms = ms.Replace("#13uRnu37:", "");
                                    throw new Exception(ms);
                                    /*_client.Close();
                                    rtb.BeginInvoke(AcceptDel, new object[] { rtb, ms });
                                    break;*/
                                }
                                rtb.BeginInvoke(AcceptDel, new object[] { rtb, ms });
                            }
                        }
                        Task.Delay(20).Wait();
                    }
                    catch (Exception ex)
                    {
                        conbutton.BeginInvoke(new Action<Button>((btn) => btn.Enabled = true), conbutton);
                        sendbutton.BeginInvoke(new Action<Button>((btn) => btn.Enabled = false), sendbutton);
                        _client.Close();
                        MessageBox.Show(ex.ToString());
                        
                        
                    }
                }
            }
            );
        }
        delegate void mssg(RichTextBox rtb, string text);

        mssg AcceptDel = (RichTextBox rtb, string text) =>
        {
            rtb.Text += text + "\n";
        };


        public void SendMessage(string MsgText)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    if (_client?.Connected == true)
                    {
                        netStream = _client.GetStream();
                        byte[] Data = Encoding.UTF8.GetBytes(MsgText);
                        await netStream.WriteAsync(Data, 0, Data.Length);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            });
        }

        /*  public void ReadMessage()
          {
              try
              {
                  if(_client)
                  netStream = _client.GetStream();
                  byte[] Data = new byte[1024];
                  Task.Delay(10).Wait();
              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.ToString());
              }
          }*/
        public void EndConnection()
        {
            try
            {
                SendMessage(_Name + " (" + _CurrentIP + ") - отключился");
                netStream.Close();
                _client.Close();
            }
            catch (Exception)
            {

            }
        }

    }
}
