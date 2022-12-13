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
            _db.LeaveTypes.Update(obj);
        }

    }
}
