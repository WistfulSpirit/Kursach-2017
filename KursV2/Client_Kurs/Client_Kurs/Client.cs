using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client_Kurs
{
    class Client
    {
        #region Var's
        TcpClient _client;
        NetworkStream netStream;

        string _ServerIP;
        public string ServIP
        {
            get { return _ServerIP; }
            set { _ServerIP = value; }
        }

        /*string _CurrentIP;
        public string CurIP
        {
            get { return _CurrentIP; }
            set { _CurrentIP = value; }
        }*/

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

        string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        #endregion

        #region Delegat's
        delegate void mssg(RichTextBox rtb, string text);
        mssg AcceptDel = (RichTextBox rtb, string text) =>
        {
            rtb.Text += text + "\n";
        };

        delegate void statText(TextBoxBase tbb, bool stat);
        statText StatText = (TextBoxBase tb, bool s) =>
        {
            tb.Enabled = s;
        };

        delegate void btn(Button button, bool stat);
        btn Stat = (Button b, bool s) =>
        {
            b.Enabled = s;
        };
        #endregion

        public Client(string _IPserver, int _PortServer, string _UserName, string _UserPass)
        {
            _ServerIP = _IPserver;
            _Port = _PortServer;
            _Name = _UserName;
            _Password = _UserPass;
        }

        public void ConnectToServer(Button conbutton, Button sendbutton, RichTextBox rtb, TextBox tb, MaskedTextBox mtbx)
        {
            _client = new TcpClient();
            Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        await _client.ConnectAsync(IPAddress.Parse(_ServerIP), _Port);
                        SendMessage("#N4m3:" + _Name + "|#p455w0r6:" + _Password);
                        if (_client.Connected)
                        {
                            conbutton.BeginInvoke(Stat, new object[] { conbutton, false });
                            rtb.BeginInvoke(StatText, new object[] { rtb, true });
                            tb.BeginInvoke(StatText, new object[] { tb, false });
                            mtbx.BeginInvoke(StatText, new object[] { mtbx, false });
                        }
                    }
                    catch (Exception ex)
                    {
                        conbutton.BeginInvoke(Stat, new object[] { conbutton, true });
                        sendbutton.BeginInvoke(Stat, new object[] { sendbutton, false });
                        rtb.BeginInvoke(StatText, new object[] { rtb, false });
                        tb.BeginInvoke(StatText, new object[] { tb, true });
                        mtbx.BeginInvoke(StatText, new object[] { mtbx, true });
                        MessageBox.Show(ex.Message);
                    }
                }
                );
        }

       /* public bool ConnectToServer()
        {
            _client = new TcpClient();
            bool res = Task<bool>.Factory.StartNew(() =>
            {
                try
                {
                    _client.Connect(IPAddress.Parse(_ServerIP), _Port);
                    IPHostEntry thisIP = Dns.GetHostEntry(Dns.GetHostName());
                    SendMessage("#N4m3:" + _Name + "|#p455w0r6:" + _Password);
                    if (_client.Connected)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
                ).Result;
            return res;
        }*/

        public bool ReConnect(string message)
        {
            _client = new TcpClient();
            bool res = Task<bool>.Factory.StartNew(() =>
            {
                try
                {
                    _client.Connect(IPAddress.Parse(_ServerIP), _Port);
                    SendMessage(message);
                    if (_client.Connected)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
                ).Result;
            return res;
        }

        public void ReadStream(RichTextBox rtb, Button conbutton, Button sendbutton, RichTextBox tosendrtb, TextBox tb, MaskedTextBox mtbx)
        {
            Task.Factory.StartNew(async () =>
            {
                NetworkStream stream;
                while (true)
                {
                    try
                    {
                        if (_client != null && _client.Connected == true)//client !=null && client.Connected = true
                        {
                            stream = _client.GetStream();
                            byte[] Data = new byte[512];
                            int bytes = await stream.ReadAsync(Data, 0, Data.Length);
                            string ms = Encoding.UTF8.GetString(Data, 0, bytes);
                            if (ms != "" && ms != "#c43k1n9")
                            {
                                if (ms.Contains("#ud3x15t:"))
                                {
                                    await stream.FlushAsync();
                                    throw new Exception(ms);
                                }
                                else if (ms.Contains("#uR4l13r:"))
                                {
                                    ms = ms.Replace("#uR4l13r:", "");
                                    throw new Exception(ms);
                                }
                                else if (ms.Contains("#13uRn37:"))
                                {
                                    ms = ms.Replace("#13uRn37:", "");
                                    throw new Exception(ms);
                                }
                                
                                rtb.BeginInvoke(AcceptDel, new object[] { rtb, ms });
                            }
                        }
                        Task.Delay(20).Wait();
                    }
                    catch (Exception ex)
                    {
                        conbutton.BeginInvoke(Stat, new object[] { conbutton, true });
                        sendbutton.BeginInvoke(Stat, new object[] { sendbutton, false });
                        tosendrtb.BeginInvoke(StatText, new object[] { tosendrtb, false });
                        tb.BeginInvoke(StatText, new object[] { tb, true });
                        mtbx.BeginInvoke(StatText, new object[] { mtbx, true });
                        if (ex.ToString().Contains("UserNotExist"))
                        {
                            conbutton.BeginInvoke(Stat, new object[] { conbutton, false });
                            tosendrtb.BeginInvoke(StatText, new object[] { tosendrtb, true });
                            tb.BeginInvoke(StatText, new object[] { tb, false });
                            mtbx.BeginInvoke(StatText, new object[] { mtbx, false });
                            if (MessageBox.Show("Пользователя с таким логином не существует, создать?", "Ошибка", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                ReConnect("#466n3wu53r:#N4m3:" + _Name + "|#p455w0r6:" + _Password);
                            }
                            else
                            {
                                ReConnect("#1w4nn4d13");
                            }
                        }
                        else
                        {
                            netStream.Close();
                            _client.Close();
                            MessageBox.Show(ex.Message, "Ошибка");
                        }
                    }
                }
            }
            );
        }



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

      /*  public void SendMessage(RichTextBox MsgText)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    if (_client?.Connected == true)
                    {
                        netStream = _client.GetStream();
                        byte[] Data = Encoding.UTF8.GetBytes(MsgText.Text);
                        await netStream.WriteAsync(Data, 0, Data.Length);
                        MsgText.BeginInvoke(AcceptDel, new object[] { MsgText, "" });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            });
        }*/
    }
}
