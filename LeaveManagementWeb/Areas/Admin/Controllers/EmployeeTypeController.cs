using LeaveManagement.DataAccess.Repository.IRepository;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Mvc;


namespace LeaveManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeTypeController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeTypeController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)

        {
            _unitOfWork = unitOfWork;

        }


        public IActionResult Index()
        {
            var EmployeeTypeList = _unitOfWork.EmployeeType.GetAll();

            return View();
        }


        public IActionResult Upsert(int? id)
        {
            EmployeeType employeeType = new();

            if (id == null || id == 0)
            {
                //create product
                //ViewBag.EmployeeList = EmployeeList;
                //ViewBag.CoverTypeList = CoverTypeList;
                return View(employeeType);
            }
            else
            {
                //update product
                employeeType = _unitOfWork.EmployeeType.GetFirstOrDefault(u => u.EmployeeTypeId == id);
                return View(employeeType);

            }



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(EmployeeType obj)
        {
            if (ModelState.IsValid)
            {

                string msg = "";


                if (obj.EmployeeTypeId == 0)
                {
                    _unitOfWork.EmployeeType.Add(obj);
                    msg = "Added";
                }
                else
                {
                    _unitOfWork.EmployeeType.Update(obj);
                    msg = "Updated";
                }

                _unitOfWork.Save();
                TempData["success"] = "Employee " + msg + " Successfully";
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
            var employeeTypeList = _unitOfWork.EmployeeType.GetAll();
            return Json(new { data = employeeTypeList });

        }

        [HttpDelete]
        public IActionResult Delete(int? id)

        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var employeeFromDb = _db.Employees.Find(id);
            var obj = _unitOfWork.EmployeeType.GetFirstOrDefault(u => u.EmployeeTypeId == id);


            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });

            }


            _unitOfWork.EmployeeType.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successfull" });
            // return RedirectToAction("Index");
        }

        #endregion
    }
}
