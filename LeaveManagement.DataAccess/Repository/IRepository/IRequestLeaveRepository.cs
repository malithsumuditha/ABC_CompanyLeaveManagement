using LeaveManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.DataAccess.Repository.IRepository
{
	public interface IRequestLeaveRepository : IRepository<RequestLeave>

	{
		void Update(RequestLeave obj);

		IEnumerable<RequestLeave> GetById(int id, string? includeProperties = null);

		void Decline(RequestLeave obj);
		void Approve(RequestLeave obj, EmployeeLeave employeeLeave);
		
	}

}
