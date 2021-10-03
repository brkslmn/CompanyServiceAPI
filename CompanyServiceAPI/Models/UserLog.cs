using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyServiceAPI.Models
{
    public class UserLog
    {

        [Key]
        public int Id { get; set; }
        public byte[] LogTime { get; set; }
        public int IpAdress { get; set; }
        public Nullable<int> UserRole { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
