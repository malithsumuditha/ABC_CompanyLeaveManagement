using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ILeaveTypeRepository LeaveType { get; }
        IEmployeeTypeRepository EmployeeType { get; }

        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}
