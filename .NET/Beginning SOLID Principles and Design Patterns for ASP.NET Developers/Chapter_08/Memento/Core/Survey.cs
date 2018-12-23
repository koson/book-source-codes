using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Memento.Core
{
    public class Survey
    {
        private SurveyState state;

        public Survey(SurveyState state)
        {
            this.state = state;
        }

        public SurveySnapshot CreateSnapshot()
        {
            SurveySnapshot snapshot = new SurveySnapshot(AppSettings.StoragePath);
            snapshot.Save(this.state);
            return snapshot;
        }

        public void RestoreSnapshot(SurveySnapshot snapshot)
        {
            this.state = snapshot.Restore();
        }

        public List<string> GetAnswers()
        {
            return this.state.Answers;
        }

        public void Submit()
        {
            using (AppDbContext db = new AppDbContext())
            {
                for (int i = 0; i < state.Questions.Count; i++)
                {
                    Answer ans = new Answer();
                    ans.QuestionId = state.Questions[i];
                    ans.AnswerText = state.Answers[i];
                    ans.SubmittedOn = DateTime.Now;
                    db.Answers.Add(ans);
                }
                db.SaveChanges();
            }
        }
    }

    

   

}
