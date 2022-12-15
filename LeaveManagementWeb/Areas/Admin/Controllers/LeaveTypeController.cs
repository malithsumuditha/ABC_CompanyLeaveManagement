using LeaveManagement.DataAccess.Repository.IRepository;
using LeaveManagement.Models;
using LeaveManagement.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LeaveManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class LeaveTypeController :Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public LeaveTypeController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)

        {
            _unitOfWork = unitOfWork;

        }

        
        public IActionResult Index()
        {
            var LeaveTypeList = _unitOfWork.LeaveType.GetAll();


            return View();
        }


        public IActionResult Upsert(int? id)
        {
            LeaveType leaveType = new();

            if (id == null || id == 0)
            {
                //create product
                //ViewBag.EmployeeList = EmployeeList;
                //ViewBag.CoverTypeList = CoverTypeList;
                return View(leaveType);
            }
            else
            {
                //update product
                leaveType = _unitOfWork.LeaveType.GetFirstOrDefault(u => u.LeaveTypeId == id);
                return View(leaveType);

            }



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(LeaveType obj)
        {
            if (ModelState.IsValid)
            {

                string msg = "";

				string leaveTypeName = obj.LeaveTypeName;

				string subLeaveTypeName = leaveTypeName.Substring(0, 6);

				if (subLeaveTypeName == "Annual")
				{
					obj.LeaveTypeName = "Annual";

				}
				else if (subLeaveTypeName == "Casual")
				{
					obj.LeaveTypeName = "Casual";
				}
				else if (subLeaveTypeName == "Medica")
				{
					obj.LeaveTypeName = "Medical";
				}


				if (obj.LeaveTypeId == 0)
                {
                    
						_unitOfWork.LeaveType.Add(obj);
                    msg = "Added";
                }
                else
                {
                    _unitOfWork.LeaveType.Update(obj);
                    msg = "Updated";
                }

                _unitOfWork.Save();
                TempData["success"] = "Leave Type " + msg + " Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);


        }

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    //var employeeFromDb = _db.Employees.Find(id);
        //    var productFromDbFirst = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == id);


        //    if (productFromDbFirst == null)
        //    {
        //        return NotFound();
        //    }



        //    _unitOfWork.Product.Remove(productFromDbFirst);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Product Deleted Successfully";
        //    return RedirectToAction("Index");


        //}


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var leaveTypeList = _unitOfWork.LeaveType.GetAll();
            return Json(new { data = leaveTypeList });

        }

        [HttpDelete]
        public IActionResult Delete(int? id)

        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var employeeFromDb = _db.Employees.Find(id);
            var obj = _unitOfWork.LeaveType.GetFirstOrDefault(u => u.LeaveTypeId == id);


            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });

            }


            _unitOfWork.LeaveType.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successfull" });
            // return RedirectToAction("Index");
        }

        #endregion
    }
}
