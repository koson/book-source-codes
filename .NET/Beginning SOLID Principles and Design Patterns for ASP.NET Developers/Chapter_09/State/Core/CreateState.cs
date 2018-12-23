using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace State.Core
{
    public class CreateState : ICampaignState
    {
        public void Process(CampaignContext context)
        {
            using (AppDbContext db = new AppDbContext())
            {
                context.Campaign.Status = "Campaign has been created";
                db.Campaigns.Add(context.Campaign);
                db.SaveChanges();
            }
            context.State = new ApprovalState();
        }
    }
}
