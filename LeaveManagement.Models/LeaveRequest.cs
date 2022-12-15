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
    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestId { get; set; }

        public int LeaveTypeId { get; set; }
        [ValidateNever]
        public LeaveType LeaveType { get; set; }
        public int EmployeeLeaveId { get; set; }

        
        [ValidateNever]
        public EmployeeLeave EmployeeLeave { get; set; }

        public DateTime DayFrom { get; set; }
        public DateTime DayTo { get; set; }

        public int? Days { get; set; }
        public bool? IsApproved { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ApprovedDate { get; set; }

        public string Comment { get; set; }



    }
}
