using PostSystem_EL.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_EL.Entities
{
    [Table("POSTMEDIA")]
    public class PostMedia: BaseNumeric<int>
    {
        public string MediaPath { get; set; }
        public long PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual UserPost UserPost { get; set; }
    }
}
