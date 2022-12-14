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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {

            _db = db;
        }

        public void Update(ApplicationUser obj)
        {
            var objFromDb = _db.applicationUsers.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                //objFromDb.UpdatedDate = DateTime.Now;
                ////obj.CreatedDate = objFromDb.CreatedDate;
                //objFromDb.TotalLeaves = obj.TotalLeaves;
                //objFromDb.LeaveTypeName = obj.LeaveTypeName;



            }
        }

        public string GetLastUserId()

        {
            var lastColumn = _db.applicationUsers.OrderBy(x => x.UserCode).LastOrDefault();
            string lastColumnId = "";
            if (lastColumn == null)
            {
                lastColumnId = "U0000";
            }
            else
            {
                lastColumnId = lastColumn.UserCode;
            }
            
            return lastColumnId;
        }
    }
}
