using PostSystem_EL.IdentityModels;
using PostSystem_EL.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PostSystem_PL.Models
{
    public class PostIndexVM
    {
        [StringLength(500)]
        [MinLength(2)]

        public long Id { get; set; }
        public string Post { get; set; }

        public string UserId { get; set; }

        public bool IsDefaultPost { get; set; } // ekstra

        public bool IsDeleted { get; set; }

        public DateTime InsertedDate { get; set; }  

        public AppUser? AppUser { get; set; }

        public List<IFormFile>? PostPictures { get; set; }

        public List<PostMediaDTO>? PostMedias { get; set; }

    }
}
