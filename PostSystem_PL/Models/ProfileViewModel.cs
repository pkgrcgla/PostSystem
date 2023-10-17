using PostSystem_EL.IdentityModels;
using PostSystem_EL.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PostSystem_PL.Models
{
    public class ProfileViewModel: AppUser
    {
        public IFormFile? ChosenPicture { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
