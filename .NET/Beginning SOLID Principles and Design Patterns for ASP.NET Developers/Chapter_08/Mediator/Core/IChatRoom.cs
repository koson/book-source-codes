using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mediator.Core
{
    public interface IChatRoom
    {
        void Login(IParticipant participant);
        void Logout(IParticipant participant);
        void Send(string from, string to, string message);
    }
}
