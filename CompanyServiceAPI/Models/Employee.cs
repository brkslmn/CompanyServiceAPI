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
           
            this.UserLog = new HashSet<UserLog>();
            
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

       
        public virtual ICollection<UserLog> UserLog { get; set; }
    }
}
