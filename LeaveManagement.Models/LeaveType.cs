using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Models
{
    public class LeaveType
    {
        [Key]
        public int LeaveTypeId { get; set; }
        [Required]
        [DisplayName("Leave Type Name")]
        public string LeaveTypeName { get; set; }
        public int TotalLeaves { get; set; }
        public DateTime CreatedDate { get; set; }  = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;


    }
}
