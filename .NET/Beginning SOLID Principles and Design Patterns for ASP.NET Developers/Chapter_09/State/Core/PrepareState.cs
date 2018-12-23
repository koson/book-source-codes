using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace State.Core
{
    public class PrepareState : ICampaignState
    {
        public void Process(CampaignContext context)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Campaign campaign = db.Campaigns.Where(o => o.Id == context.Campaign.Id).SingleOrDefault();
                campaign.Status = "Material for the campaign has been ordered";
                db.SaveChanges();
            }
            context.State = new RunState();
        }
    }
}
