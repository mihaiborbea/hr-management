using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfClient.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string access_token { get; set; }
    }
}