using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace Proxy.Core
{
    [Route("api/[controller]")]
    public class ServiceController : Controller,ICustomerApi
    {

        [HttpGet]
        public List<Customer> Get()
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.Customers.ToList();
            }
        }

        [HttpGet("{customerid}")]
        public Customer Get(string customerid)
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.Customers.Where(m => m.CustomerID == customerid).SingleOrDefault();
            }
        }

        [HttpPost]
        public void Post([FromBody]Customer obj)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Customers.Add(obj);
                db.SaveChanges();
            }
        }

        [HttpPut("{customerid}")]
        public void Put(string customerid, [FromBody]Customer obj)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        [HttpDelete("{customerid}")]
        public void Delete(string customerid)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Customer obj = db.Customers.Where(m => m.CustomerID == customerid).SingleOrDefault();
                db.Customers.Remove(obj);
                db.SaveChanges();
            }
        }
    }
}
