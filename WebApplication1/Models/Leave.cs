using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Leave
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public LeaveType Type { get; set; }
    }

    public enum LeaveType { Sickness, Rest, Parental, Unpaid };
}
