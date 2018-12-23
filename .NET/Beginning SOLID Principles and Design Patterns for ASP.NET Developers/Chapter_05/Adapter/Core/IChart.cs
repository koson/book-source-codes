using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Drawing;


namespace Adapter.Core
{
    public interface IChart
    {
        string Title { get; set; }
        List<string> XData { get; set; }
        List<int> YData { get; set; }
        Bitmap GenerateChart();
    }

}
