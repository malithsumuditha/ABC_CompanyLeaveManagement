using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

		public string UserCode { get; set; }

        public int? EmployeeTypeId { get; set; }
        public EmployeeType Employee { get; set; }


    }
}
