using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server_Kurs
{
    class ConnectedUser
    {
        public TcpClient _client { get; set; }
        public string _name { get; set; }


        public ConnectedUser(TcpClient cl, string nick)
        {
            _client = cl;
            _name = nick;
        }
    }
}
