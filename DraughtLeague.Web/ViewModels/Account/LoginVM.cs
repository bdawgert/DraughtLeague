using System.ComponentModel.DataAnnotations;

namespace DraughtLeague.Web.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}