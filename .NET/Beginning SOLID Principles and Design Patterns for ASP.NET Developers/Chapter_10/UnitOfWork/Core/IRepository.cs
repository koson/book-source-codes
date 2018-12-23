using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWork.Core
{
    public interface IRepository<T1,T2> where T1:class
    {
        List<T1> SelectAll();
        T1 SelectByID(T2 id);
        void Insert(T1 obj);
        void Update(T1 obj);
        void Delete(T2 id);
        void Save();
    }
}
