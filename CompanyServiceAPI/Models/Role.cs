using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyServiceAPI.Models
{
    public class Role
    {
        public Role()
        {
            this.Employee = new HashSet<Employee>();
        }
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }


        public virtual ICollection<Employee> Employee { get; set; }
    }
}
