using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Microsoft.Data.Entity;
using Module.Core;


namespace Namespace.Controllers
{
    [Route("api/[controller]")]
    public class CustomerService : Controller
    {
        [HttpGet]
        public List<Customer> Get()
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.Customers.ToList();
            }
        }

        [HttpGet("{id}")]
        public Customer Get(string id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.Customers.Where(i => i.CustomerID == id).SingleOrDefault();
            }
        }

        [HttpPost]
        public void Post([FromBody]Customer obj)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Entry(obj).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Customer obj)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Customer obj = db.Customers.Where(i => i.CustomerID == id).SingleOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}
