﻿using PostSystem_EL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_EL.ViewModels
{
    public class PostTagDTO
    {
        public long Id { get; set; }
        public DateTime InsertedDate { get; set; }
        public string Tag { get; set; }
        public long PostId { get; set; }
        public UserPost? UserPost { get; set; }
    }
}
