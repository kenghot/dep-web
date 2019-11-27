using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
     [Serializable]
    public class DecimalDropDownListData
    {
        public Decimal Value { get; set; }
        public String Text { get; set; }
        public decimal OrderNo { get; set; }
        public decimal? ParentID { get; set; }
    }
}
