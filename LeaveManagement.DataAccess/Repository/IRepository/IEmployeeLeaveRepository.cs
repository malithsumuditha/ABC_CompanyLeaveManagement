using LeaveManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.DataAccess.Repository.IRepository
{
    public interface IEmployeeLeaveRepository : IRepository<EmployeeLeave>
    {
        void Update(EmployeeLeave obj);
        void UpdateGetLeaves(EmployeeLeave obj);

    }
}
