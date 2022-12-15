using LeaveManagement.DataAccess.Data;
using LeaveManagement.DataAccess.Repository.IRepository;
using LeaveManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.DataAccess.Repository
{
    public class EmployeeLeaveRepository : Repository<EmployeeLeave>, IEmployeeLeaveRepository
    {
        private ApplicationDbContext _db;

        public EmployeeLeaveRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(EmployeeLeave obj)
        {
            var objFromDb = _db.EmployeeLeaves.FirstOrDefault(u => u.UserId == obj.UserId);
            if (objFromDb != null)
            {
                objFromDb.AnnualLeaves = obj.AnnualLeaves;
                objFromDb.CasualLeaves = obj.CasualLeaves;
                objFromDb.MedicalLeaves = obj.MedicalLeaves;
                



            }
        }

        public void UpdateGetLeaves(EmployeeLeave obj)

        {
            var objFromDb = _db.EmployeeLeaves.FirstOrDefault(u => u.UserId == obj.UserId);
            if (objFromDb != null)
            {
                objFromDb.GetAnnualLeaves = obj.AnnualLeaves;
                objFromDb.GetCasualLeaves = obj.CasualLeaves;
                objFromDb.GetMedicalLeaves = obj.MedicalLeaves;




            }
        }

    }
}
