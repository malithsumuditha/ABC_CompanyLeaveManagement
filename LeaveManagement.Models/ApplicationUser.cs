using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        
    }
}
