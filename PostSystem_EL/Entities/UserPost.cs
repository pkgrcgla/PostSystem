using PostSystem_EL.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_EL.Entities
{
    [Table("USERPOST")]
    public class UserPost:BaseNumeric<long>
    {
        [StringLength(200)]
        public string Post {get;set;}
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }
    }
}
