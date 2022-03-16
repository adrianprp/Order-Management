using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiuneComenzi
{
    class ClientInfo
    {
        static string id;
        static string name;

        public ClientInfo() { }

        public ClientInfo(string _name, string _id)
        {
            name = _name;
            id = _id;
        }

        public string getClientID()
        {
            return id;
        }

        public string getClientName()
        {
            return name;
        }
    }
}
