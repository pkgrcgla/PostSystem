﻿using PostSystem_EL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_EL.ViewModels
{
    public class PostMediaDTO
    {
        public int Id { get; set; }
        public DateTime InsertedDate { get; set; }
        public string MediaPath { get; set; }
        public int PostId { get; set; }
        public UserPost? UserPost { get; set; }
    }
}
