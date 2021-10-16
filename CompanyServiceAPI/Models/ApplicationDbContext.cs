using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyServiceAPI.Models;

namespace CompanyServiceAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserLog> UserLog { get; set; }
        public DbSet<User> User { get; set; }
        
        
    }
}
