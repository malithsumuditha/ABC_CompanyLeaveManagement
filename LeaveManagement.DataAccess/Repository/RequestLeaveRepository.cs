using LeaveManagement.DataAccess.Data;
using LeaveManagement.DataAccess.Repository.IRepository;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Web.Mvc;

namespace LeaveManagement.DataAccess.Repository
{
	public class RequestLeaveRepository : Repository<RequestLeave>, IRequestLeaveRepository
	{
		private ApplicationDbContext _db;

		public RequestLeaveRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
			//_db.RequestLeaves.Include(u => u.LeaveType);

		}

		public void Update(RequestLeave obj)
		{
			var objFromDb = _db.RequestLeaves.FirstOrDefault(u => u.LeaveRequestId == obj.LeaveRequestId);
			if (objFromDb != null)
			{
				objFromDb.ApprovedDate = DateTime.Now;
				objFromDb.IsApproved = true;

				//obj.CreatedDate = objFromDb.CreatedDate;

			}
		}

		
		public void Decline(RequestLeave obj)
		{
			var objFromDb = _db.RequestLeaves.FirstOrDefault(u => u.LeaveRequestId == obj.LeaveRequestId);
			if (objFromDb != null)
			{
				
				objFromDb.IsApproved = false;

				//obj.CreatedDate = objFromDb.CreatedDate;

			}
		}


		public void Approve(RequestLeave obj, EmployeeLeave employeeLeave)

		{
			var objFromDb = _db.RequestLeaves.FirstOrDefault(u => u.LeaveRequestId == obj.LeaveRequestId);
			var employeeFromDb =  _db.EmployeeLeaves.FirstOrDefault(u => u.Id == employeeLeave.Id);

			

			if (objFromDb != null)
			{

				objFromDb.IsApproved = true;
				objFromDb.ApprovedDate = DateTime.Now;

				//obj.CreatedDate = objFromDb.CreatedDate;

			}

			int leaveTypeId = obj.LeaveTypeId;

			var leaveTypeFromDb = _db.LeaveTypes.FirstOrDefault(u => u.LeaveTypeId == leaveTypeId);



			string leaveType = leaveTypeFromDb.LeaveTypeName;
			string colName;


			if (leaveType == "Annual")
			{

				if (employeeFromDb.GetAnnualLeaves >= employeeFromDb.AnnualLeaves)
				{
					Decline(objFromDb);
					
				}
				else
				{
					employeeFromDb.GetAnnualLeaves = employeeFromDb.GetAnnualLeaves + obj.Days;
				}

				

			}
			else if (leaveType == "Casual"){


				if (employeeFromDb.GetCasualLeaves >= employeeFromDb.CasualLeaves)
				{
					Decline(objFromDb);
				}
				else
				{
					employeeFromDb.GetCasualLeaves = employeeFromDb.GetCasualLeaves + obj.Days;
				}


				
			}
			else
			{

				if (employeeFromDb.GetCasualLeaves >= employeeFromDb.MedicalLeaves)
				{
					Decline(objFromDb);
				}
				else
				{
					employeeFromDb.GetMedicalLeaves = employeeFromDb.GetMedicalLeaves + obj.Days;
				}

				

			}

			

		}



		public IEnumerable<RequestLeave> GetById(int id, string? includeProperties = null)
		{

			var objFromDb = _db.RequestLeaves.Where(x => x.EmployeeLeaveId == id);


					return objFromDb;

		   

		}


	}
}
