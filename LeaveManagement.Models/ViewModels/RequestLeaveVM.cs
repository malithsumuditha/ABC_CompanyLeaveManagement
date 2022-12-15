using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Models.ViewModels
{
	public class RequestLeaveVM
	{
		public RequestLeave RequestLeave{get; set;}


		[ValidateNever]
		public IEnumerable<SelectListItem> LeaveTypeList { get; set; }

	}
}
