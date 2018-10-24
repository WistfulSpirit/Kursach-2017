using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Server_Kurs
{
    class Server
    {
        static int port;
        TcpListener listener;
        List<ConnectedUser> UList = new List<ConnectedUser>();
        public bool error { get; set; }

        public Server(int prt)
        {
            port = prt;
        }


        public void StartListen(RichTextBox rtb,Button startbutton)
        {
            error = false;
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();//начинаем слушать порт по умолчанию
                //начинаем "обработку" нового подключения в новом потоке
                error = false;
                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        TcpClient client = listener.AcceptTcpClient();//принимаем запрос на подключение
                        Task.Factory.StartNew(async () =>
                        {
                            NetworkStream stream = client.GetStream();
                            byte[] Data = new byte[1024];
                            int bytes = await stream.ReadAsync(Data, 0, Data.Length);
                            string ms = Encoding.UTF8.GetString(Data, 0, bytes);
                            string nick = "";
                            string pass = null;
                            bool createnew = false;
                            ConnectedUser user;
                            while (client.Connected)
                            {
                                if (ms.Contains("#1w4nn4d13"))
                                {
                                    client.Close();
                                    stream.Close();
                                    break;
                                }
                                if (ms.Contains("#N4m3:") && ms.Contains("#p455w0r6:"))
                                {
                                    if (ms.Contains("#466n3wu53r:"))
                                    {
                                        ms = ms.Replace("#466n3wu53r:", "");
                                        createnew = true;
                                    }
                                    string[] info = ms.Split('|');
                                    nick = info[0].Replace("#N4m3:", "").ToLower();
                                    pass = info[1].Replace("#p455w0r6:", "");
                                    if (!Authorization.Exist(nick))
                                    {
                                        if (createnew)
                                        {
                                            user = new ConnectedUser(client, nick, pass);
                                            UList.Add(user);
                                            Authorization.Add_User(user._name, user._password);
                                            rtb.BeginInvoke(AcceptDel, new object[] { rtb, "Connected: " + nick });
                                            break;
                                        }
                                        else
                                        {
                                            byte[] error = Encoding.UTF8.GetBytes("#ud3x15t:UserNotExist");
                                            await stream.WriteAsync(error, 0, error.Length);
                                            client.Close();
                                            stream.Close();
                                        }
                                    }
                                    else
                                    {
                                        if (!UList.Exists((s => s._name == nick)))
                                        {
                                            if (Authorization.Passed(nick, pass))
                                            {

                                                user = new ConnectedUser(client, nick, pass);
                                                UList.Add(user);
                                                rtb.BeginInvoke(AcceptDel, new object[] { rtb, "Connected: " + nick });
                                                break;
                                            }
                                            else
                                            {
                                                byte[] error = Encoding.UTF8.GetBytes("#uR4l13r:Неверный логин или пароль!");
                                                await stream.WriteAsync(error, 0, error.Length);
                                                client.Close();
                                                stream.Close();
                                            }
                                        }
                                        else
                                        {
                                            byte[] error = Encoding.UTF8.GetBytes("#13uRn37:Пользователь уже подключен!");
                                            await stream.WriteAsync(error, 0, error.Length);
                                            client.Close();
                                            stream.Close();
                                        }
                                    }
                                }
                            }
                            while (client.Connected)
                            {
                                try
                                {
                                    stream = client.GetStream();
                                    bytes = await stream.ReadAsync(Data, 0, Data.Length);
                                    ms = Encoding.UTF8.GetString(Data, 0, bytes);
                                    if (ms == "#1md0n3")
                                    {
                                        var u = UList.Find(s => s._name == nick);
                                        rtb.BeginInvoke(AcceptDel, new object[] { rtb, "Disconnected " + u._name });
                                        UList.Remove(u);
                                        client.Close();
                                        stream.Close();
                                        break;
                                    }
                                    await SendToAll(ms, rtb);
                                }
                                catch (Exception)
                                {
                                    await SendToAll("#c43k1n9", rtb);
                                }
                            }
                        }
                        );
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                error = true;
                startbutton.BeginInvoke(new Action<Button>((btn) => btn.Enabled = true), startbutton);
            }
        }

        public void CheckList(RichTextBox rtb)
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    if (UList.Count != 0)
                    {
                        await SendToAll("#c43k1n9", rtb);
                    }
                    Task.Delay(1000).Wait();
                }
            });
        }

        async Task SendToAll(string message, RichTextBox rtb)
        {
            await Task.Factory.StartNew(async () =>
            {
                for (int i = 0; i < UList.Count; i++)
                {
                    if (UList[i]._client.Connected)
                    {
                        var SW = UList[i]._client.GetStream();
                        byte[] Data = Encoding.UTF8.GetBytes(message);
                        //netStream.Write(Data, 0, Data.Length);
                        await SW.WriteAsync(Data, 0, Data.Length);
                    }
                    else
                    {
                        rtb.BeginInvoke(AcceptDel, new object[] { rtb, "Disconnected " + UList[i]._name });
                        UList[i]._client.Close();
                        UList.Remove(UList[i]);
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

    }
}
