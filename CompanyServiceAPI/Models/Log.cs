using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyServiceAPI.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public System.DateTime LogTime { get; set; }
        public int IpAdress { get; set; }
        public string Description { get; set; }
    }
}
