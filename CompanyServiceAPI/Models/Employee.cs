using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyServiceAPI.Models
{
    
    public partial class Employee
    {
        public Employee()
        {
            this.Department = new HashSet<Department>();
            this.UserLog = new HashSet<UserLog>();
            this.Role = new HashSet<Role>();
        }
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public System.DateTime StartsDateOfWork { get; set; }
        public float Salary { get; set; }
        public int RoleId { get; set; }

        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<UserLog> UserLog { get; set; }
        public virtual ICollection<Role> Role { get; set; }
    }
}
