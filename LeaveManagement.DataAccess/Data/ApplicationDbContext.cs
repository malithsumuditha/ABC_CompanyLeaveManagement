using LeaveManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LeaveManagement.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            

        }

        public DbSet<LeaveType> LeaveTypes { get; set; }

        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<EmployeeLeave> EmployeeLeaves { get; set; }
        //public DbSet<LeaveRequest> LeaveRequests { get; set; }

        public DbSet<RequestLeave> RequestLeaves { get; set; }


    }
}
