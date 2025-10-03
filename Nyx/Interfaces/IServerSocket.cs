using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Interfaces
{
    public interface IServerSocket
    {
        void Start();
        void Stop();
        void Mintain();
        void OnReciveData();
    }
}
