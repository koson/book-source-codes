using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Core
{
    public interface ITradingRepository
    {
        List<StockTradingEntry> SelectAll();
        List<StockTradingEntry> SelectForUser(string user);
        void Insert(StockTradingEntry obj);
        void Update(StockTradingEntry obj);
        void Delete(int id);
    }
}
