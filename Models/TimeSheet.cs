using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    public partial class TimeSheet
    {
        public int? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? BreakStart { get; set; }
        public DateTime? BreakEnd { get; set; }
        public DateTime EndTime { get; set; }
        public int? Employee { get; set; }
    }
}
