﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.API.Requests
{
    public class LoginRequest
    {
        public string UserCode { get; set; }
        public string Password { get; set; }

    }
    public class UploadImageRequest
    {
        /// <summary>
        /// null = insert, not null = edit
        /// </summary>
        public decimal? ImgId { get; set; }
        [Required]
        public decimal DataKey { get; set; }
        /// <summary>
        /// "ACTIVITYIMG", "USERIMG"
        /// </summary>
        [Required]
        public string GroupCode { get; set; }
        [Required]
        public string Base64Data { get; set; }
    }
}
