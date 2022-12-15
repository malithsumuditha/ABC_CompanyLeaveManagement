using LeaveManagement.DataAccess.Data;
using LeaveManagement.DataAccess.Repository.IRepository;
using LeaveManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.DataAccess.Repository
{
    public class EmployeeTypeRepository : Repository<EmployeeType>, IEmployeeTypeRepository
    {
        private ApplicationDbContext _db;

        public EmployeeTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public void Update(EmployeeType obj)
        {
            var objFromDb = _db.EmployeeTypes.FirstOrDefault(u => u.EmployeeTypeId == obj.EmployeeTypeId);
            if (objFromDb != null)
            {
                objFromDb.UpdatedDate = DateTime.Now;
                objFromDb.CasualLeaves = obj.CasualLeaves;
                objFromDb.MedicalLeaves = obj.MedicalLeaves;
                objFromDb.AnnualLeaves = obj.AnnualLeaves;

                //obj.CreatedDate = objFromDb.CreatedDate;
                
                objFromDb.EmployeeTypeName = obj.EmployeeTypeName;



            }



        }



        public void AllocateLeaves( EmployeeType obj, int? id)
            

        {
            var objFromDb = _db.EmployeeLeaves.Where(u => u.EmployeeTypeId == id);

            if (objFromDb != null)
            {
                
                foreach (EmployeeLeave element in objFromDb)
                {
                   
                    element.AnnualLeaves= obj.AnnualLeaves;
                    element.CasualLeaves = obj.CasualLeaves;
                    element.MedicalLeaves = obj.MedicalLeaves;

                 
                   


                }


            }



        }

    }
}
