using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    [Serializable]
    public class KendoChart
    {
        public List<KendoChartSerie> series { get; set; }
    }
    public class KendoChartSerie
    {
        public string type { get; set; }
        public List<KendoChartData> data { get; set; }
    }
    public class KendoChartData
    {
        public string category { get; set; }
        public decimal value { get; set; }
        public string color { get; set; }
        public string remark { get; set; }
    }
}
