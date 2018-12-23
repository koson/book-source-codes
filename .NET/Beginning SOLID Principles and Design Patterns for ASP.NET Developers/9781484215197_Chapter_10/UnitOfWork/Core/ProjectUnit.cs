using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWork.Core
{
    public class ProjectUnit
    {
        private AppDbContext db;
        private ProjectRepository projectRepository;
        private TeamMemberRepository teamRepository;

        public ProjectUnit()
        {
            db = new AppDbContext();
            projectRepository = new ProjectRepository(db);
            teamRepository = new TeamMemberRepository(db);
        }

        public ProjectRepository ProjectRepository
        {
            get
            {
                return this.projectRepository;
            }
        }

        public TeamMemberRepository TeamRepository
        {
            get
            {
                return this.teamRepository;
            }
        }

        public void CreateProject()
        {
            db.SaveChanges();
        }
    }
}
