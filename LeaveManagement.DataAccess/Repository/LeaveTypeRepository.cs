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
    public class LeaveTypeRepository : Repository<LeaveType>, ILeaveTypeRepository
    {
        private ApplicationDbContext _db;

        public LeaveTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(LeaveType obj)
        {
            var objFromDb = _db.LeaveTypes.FirstOrDefault(u => u.LeaveTypeId == obj.LeaveTypeId);
            if (objFromDb != null)
            {
                objFromDb.UpdatedDate = DateTime.Now;
                //obj.CreatedDate = objFromDb.CreatedDate;
                objFromDb.TotalLeaves = obj.TotalLeaves;
                objFromDb.LeaveTypeName = obj.LeaveTypeName;
                
                

            }
        }

        public int Test()
        {
            var lastColumn = _db.LeaveTypes.OrderBy(x => x.LeaveTypeId).LastOrDefault();
            int lastColumnId =  lastColumn.LeaveTypeId;
            return lastColumnId;
        }

    }
}
