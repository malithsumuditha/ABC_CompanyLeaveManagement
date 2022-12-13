﻿using LeaveManagement.DataAccess.Data;
using LeaveManagement.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            LeaveType = new LeaveTypeRepository(_db);
            EmployeeType = new EmployeeTypeRepository(_db);

        }

        public ILeaveTypeRepository LeaveType { get; private set; }
        public IEmployeeTypeRepository EmployeeType { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
