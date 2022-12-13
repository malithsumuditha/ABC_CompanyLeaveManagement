using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models
{
    public class EmployeeType
    {
        [Key]
        public int EmployeeTypeId { get; set; }
        [Required]
        [DisplayName("Employee Type")]
        public string EmployeeTypeName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        [DisplayName("Casual Leaves")]
        [Range(1, 365)]
        public int? CasualLeaves { get; set; }
        [DisplayName("Medical Leaves")]
        [Range(1, 365)]
        public int? MedicalLeaves { get; set; }
        [DisplayName("Annual Leaves")]
        [Range(1, 365)]
        public int? AnnualLeaves { get; set; }



        

    }
}
