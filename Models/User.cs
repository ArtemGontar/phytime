using System;
using System.Collections.Generic;

namespace Phytime.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Feed> Feeds { get; set; } = new List<Feed>();
    }
}
