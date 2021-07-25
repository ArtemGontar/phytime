using System.ComponentModel.DataAnnotations;

namespace Phytime.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords should be equals")]
        public string ConfirmPassword { get; set; }
    }
}
