using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels
{
    public class SignupVM
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Please enter your full name")]
        public string Name { get; set; }


        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Please enter your email address")]
        public string Email { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)] // this hides when you're typing in the password
        public string Password { get; set; }


        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)] 
        [Compare("Password", ErrorMessage ="Your passwords do not match")]
        public string ConfirmPassword { get; set;}
    }
}
