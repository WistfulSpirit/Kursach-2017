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
        TcpListener listener;// = new TcpListener(IPAddress.Any, port);
        List<ConnectedUser> UList = new List<ConnectedUser>();

        public Server(int prt)
        {
            port = prt;
        }

        public void StartListen(RichTextBox rtb)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();//начинаем слушать порт по умолчанию
                //начинаем "обработку" нового подключения в новом потоке
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
                            while (client.Connected)
                            {
                                if (ms.Contains("#N4m3: "))
                                {
                                    nick = ms.Replace("#N4m3: ", "");

                                    if (UList.Find(s => s._name == nick) == null)
                                    {
                                        UList.Add(new ConnectedUser(client, nick));
                                        rtb.BeginInvoke(AcceptDel, new object[] { rtb, "Connected: " + nick });
                                        break;
                                    }
                                    else
                                    {
                                        byte[] error = Encoding.UTF8.GetBytes("#13uRnu37:Пользователь с таким именем уже существует!");
                                        await stream.WriteAsync(error, 0, error.Length);
                                        client.Close();
                                        stream.Close();
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
                                        break;
                                    }
                                    await SendToAll(ms, rtb);
                                }
                                catch
                                {
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

        public static void ListenPort()
        {
        }
    }
}
