using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyServiceAPI.Models
{
    public class Department
    {
        public Department()
        {
            this.Employee = new HashSet<Employee>();
        }
        [Key]
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentLoc { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
        //public int CompanyId{get; set;}
        public Company Company {get; set;}
    }
}
