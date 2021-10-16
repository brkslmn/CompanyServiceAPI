using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyServiceAPI.Models
{
    public partial class Company
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string WorkField { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public ICollection<Department> Department { get; set; }
    }
}
