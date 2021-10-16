using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyServiceAPI.Models
{
    public partial class Role
    {
        public Role()
        {
            this.User = new HashSet<User>();
        }
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
