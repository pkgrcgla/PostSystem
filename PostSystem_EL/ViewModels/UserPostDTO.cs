using PostSystem_EL.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_EL.ViewModels
{
    public class UserPostDTO
    {
        public long Id { get; set; }
        public DateTime InsertedDate { get; set; }
        [StringLength(500)]
        [MinLength(2)]
        public string Post { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
