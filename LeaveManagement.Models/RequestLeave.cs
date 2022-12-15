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
    public class RequestLeave
    {
        [Key]
        public int LeaveRequestId { get; set; }

        public int LeaveTypeId { get; set; }

        
        [ValidateNever]
        public LeaveType LeaveType { get; set; }


        public int EmployeeLeaveId { get; set; }


        [ValidateNever]
        public EmployeeLeave EmployeeLeave { get; set; }

        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DayFrom { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime DayTo { get; set; }

        [Required]
        [Range(1,100)]
        public int Days { get; set; }
        public bool? IsApproved { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ApprovedDate { get; set; }

        public string? Comment { get; set; }
    }
}
