using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    [Serializable]
    public class KendoAttachment
    {
        public String id { get; set; }
        public String tempId { get; set; }
        public String name { get; set; }
        public String extension { get; set; }
        public int size { get; set; }
        //kenghot
        public String fieldName { get; set; }
    }
    public class KendoAttachmentAPI
    {
        public String id { get; set; }
        public String tempId { get; set; }
        public String name { get; set; }
        public String extension { get; set; }
        public int size { get; set; }
        //kenghot
        public String fieldName { get; set; }
    }
}
