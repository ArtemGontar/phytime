using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Phytime.ViewModels.AccountModels
{
    public class LoginModel
    {
        private string _password;
        [Required(ErrorMessage = "Email is empty")]
        [RegularExpression(@"\S*@{1}\S*", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is empty")]
        [DataType(DataType.Password)]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = GetHashString(value);
            }
        }

        private string GetHashString(string s)
        {
            if(s == null)
            {
                return null;
            }
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return new Guid(hash).ToString();
        }
    }
}
