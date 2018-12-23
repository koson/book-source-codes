using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observer.Core
{
    public interface IForumNotifier
    {
        void Subscribe(IForumObserver observer);
        void Unsubscribe(IForumObserver observer);
        void Notify(ForumPost post);
    }
}
