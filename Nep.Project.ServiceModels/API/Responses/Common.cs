﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.API.Responses
{
    public class UploadImageResponse
    {
        public decimal ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImageFullPath { get; set; }
    }
}
