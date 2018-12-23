using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;



namespace Facade.Core
{
    [Route("api/[controller]")]
    public class ServiceAController : Controller
    {
        [HttpGet("{isbn}")]
        public Book Get(string isbn)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var query = from b in db.Books
                            where b.ISBN == isbn && b.Source=="Book Store 1"
                            select b;
                return query.SingleOrDefault();
            }
        }
    }
}
