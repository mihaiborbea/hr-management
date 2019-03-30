using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Department Department { get; set; }
        public DateTime HireDate { get; set; }
        public List<Leave> Leaves { get; set; }
    }

    public enum Department { Marketing, Management };
}
