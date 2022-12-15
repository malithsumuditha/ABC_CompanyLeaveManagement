using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Models
{
    public class EmployeeLeave
    {
        [Key]
        public int Id { get; set; } 
        public int EmployeeTypeId { get; set; }
        [ValidateNever]
        public EmployeeType EmployeeType { get; set; }
        public int? AnnualLeaves { get; set; }
        public int? CasualLeaves { get; set; }
        public int? MedicalLeaves { get; set; }
        public int? GetAnnualLeaves { get; set; }
        public int? GetCasualLeaves { get; set; }
        public int? GetMedicalLeaves { get; set; }
        public string? UserId { get; set; }
        public string? UserCode { get; set; }

		
		[ValidateNever]
		[ForeignKey("UserCode")]
		public ApplicationUser ApplicationUser { get; set; }

        //[ValidateNever]
        //[ForeignKey("UserCode")]
        //public ApplicationUser ApplicationUser { get; set; }


    }
}
