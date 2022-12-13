using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Models
{
    public class EmployeeLeaves
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

        public int UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        

    }
}
