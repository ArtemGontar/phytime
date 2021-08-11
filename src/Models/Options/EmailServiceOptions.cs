using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phytime.Models
{
    public class EmailServiceOptions
    {
        public const string EmailService = "EmailService";

        public string SMTP { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DefaultConnection { get; set; }
    }
}
