using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    public partial class Employee
    {
        public int? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Name { get; set; }
        public int? DepartmentId { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Password { get; set; }
        public int? Sex { get; set; }
        public bool? IsManager { get; set; }
        public string? UserName { get; set; }
    }
}
