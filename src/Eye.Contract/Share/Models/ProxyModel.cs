using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Contract.Share.Models
{
    public class ProxyModel
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CheckStatus { get; set; }

        public ProxyModel(string ip, string port, string username, string password)
        {
            IP = ip;
            Port = port;
            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            return $"{IP}:{Port}:{Username}:{Password}";
        }
    }
}
