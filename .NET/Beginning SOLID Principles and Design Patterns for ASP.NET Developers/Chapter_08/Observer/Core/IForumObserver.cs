using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observer.Core
{
    public interface IForumObserver
    {
        void Update(ForumPost post);
    }
}
