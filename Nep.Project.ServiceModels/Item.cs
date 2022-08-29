using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    [Serializable]
    public class Item
    {
        public Decimal ITEMID { get; set; }
        public String ITEMGROUP { get; set; }
        public String ITEMNAME { get; set; }
        public Decimal ORDERNO { get; set; }
        public Boolean ISACTIVE { get; set; }

        public Boolean ISDELETE { get; set; }

    }
    [Serializable]
    public class ItemList
    {
        public Decimal ITEMID { get; set; }

        [Display(Name = "Item_Group", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ITEMGROUP { get; set; }

        [Display(Name = "Item_Name", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string ITEMNAME { get; set; }

        [Display(Name = "Item_Createby", ResourceType = typeof(Nep.Project.Resources.Model))]

        public string CREATEDBY { get; set; }
        public DateTime? CREATEDDATE { get; set; }

        [Display(Name = "Item_Updateby", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string UPDATEDBY { get; set; }

        public DateTime? UPDATEDDATE { get; set; }
        public Decimal ORDERNO { get; set; }
        public Boolean ISACTIVE { get; set; }

        public string ISACTIVESTR { get; set; }
        public bool IsDeletable { get; set; }
    }
}
