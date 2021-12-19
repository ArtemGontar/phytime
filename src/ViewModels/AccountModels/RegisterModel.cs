using System.ComponentModel.DataAnnotations;

namespace Phytime.ViewModels.AccountModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is empty")]
        [RegularExpression(@"\S*@{1}\S*", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords should be equals")]
        public string ConfirmPassword { get; set; }
    }
}
