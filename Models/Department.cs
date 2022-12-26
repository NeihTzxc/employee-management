using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    public partial class Department
    {
        public int? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Name { get; set; }
    }
}
