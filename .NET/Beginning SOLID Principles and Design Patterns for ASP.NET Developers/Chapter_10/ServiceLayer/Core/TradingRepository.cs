using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Core
{
    public class TradingRepository:ITradingRepository
    {
        public List<StockTradingEntry> SelectAll()
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.StockTradingLog.ToList();
            }
        }

        public List<StockTradingEntry> SelectForUser(string user)
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.StockTradingLog.Where(i => i.UserName == user).ToList();
            }
        }

        public void Insert(StockTradingEntry obj)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.StockTradingLog.Add(obj);
                db.SaveChanges();
            }
        }

        public void Update(StockTradingEntry obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
