using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace State.Core
{
    public class ApprovalState : ICampaignState
    {
        public void Process(CampaignContext context)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Campaign campaign = db.Campaigns.Where(o => o.Id == context.Campaign.Id).SingleOrDefault();
                campaign.Status = "Campaign has been approved";
                db.SaveChanges();
            }
            context.State = new PrepareState();
        }
    }
}
