using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_EL.Entities
{
    [Table("POSTTAG")]
    public class PostTag: BaseNumeric<int>
    {
        public string Tag { get; set; } 
        public long PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual UserPost UserPost { get; set; }
    }
}
