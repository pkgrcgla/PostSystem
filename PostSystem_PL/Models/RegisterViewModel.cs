using System.ComponentModel.DataAnnotations;

namespace PostSystem_PL.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor!")]
        public string PasswordConfirm { get; set; }

        public string Username { get; set; }
    }
}
