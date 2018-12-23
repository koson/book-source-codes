using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Core
{
    [Serializable]
    public class SurveyState
    {
        public List<int> Questions { get; set; }
        public List<string> Answers { get; set; }
    }
}
