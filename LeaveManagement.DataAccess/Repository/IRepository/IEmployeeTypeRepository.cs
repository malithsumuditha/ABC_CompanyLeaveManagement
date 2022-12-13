using LeaveManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LeaveManagement.DataAccess.Repository.IRepository
{
    public interface IEmployeeTypeRepository : IRepository<EmployeeType>
    {
        void Update(EmployeeType obj);
    }
}
