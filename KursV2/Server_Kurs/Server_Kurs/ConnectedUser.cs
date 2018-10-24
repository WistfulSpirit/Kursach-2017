using System.Net.Sockets;

namespace Server_Kurs
{
    class ConnectedUser
    {
        public TcpClient _client { get; set; }
        public string _name { get; set; }
        public string _password { get; set; }

        public ConnectedUser()
        { }

        public ConnectedUser(TcpClient cl, string nick, string password)
        {
            _client = cl;
            _name = nick;
            _password = password;
        }
    }
}
