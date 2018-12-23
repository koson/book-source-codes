using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.Entity;


namespace UnitOfWork.Core
{
    public class TeamMemberRepository:IRepository<TeamMember,int>,IDisposable
    {
        private AppDbContext db;

        public TeamMemberRepository(AppDbContext db)
        {
            this.db = db;
        }

        public List<TeamMember> SelectAll()
        {
            return db.TeamMembers.ToList();
        }

        public TeamMember SelectByID(int id)
        {
            return db.TeamMembers.Where(c => c.TeamMemberID == id).SingleOrDefault();
        }

        public void Insert(TeamMember obj)
        {
            db.TeamMembers.Add(obj);
        }

        public void Update(TeamMember obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            TeamMember obj = db.TeamMembers.Where(c => c.TeamMemberID == id).SingleOrDefault();
            db.Entry(obj).State = EntityState.Deleted;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
