using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    [Serializable]
    public class Attachment
    {
        public Decimal AttachmentID { get; set; }
        public string AttachmentFileName { get; set; }
        public string PathName { get; set; }
        public Decimal FileSize { get; set; }
    }
}
