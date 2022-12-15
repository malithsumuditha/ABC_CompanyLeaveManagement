using LeaveManagement.DataAccess.Repository.IRepository;
using LeaveManagement.Models;
using LeaveManagement.Models.ViewModels;
using LeaveManagement.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace LeaveManagementWeb.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public LeaveRequestController(IUnitOfWork unitOfWork)


        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            

            return View();
        }



        public IActionResult Upsert(int? id)
        {
            RequestLeaveVM requestLeaveVM = new()

            {
                RequestLeave = new(),

                LeaveTypeList = _unitOfWork.LeaveType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.LeaveTypeName,
                    Value = i.LeaveTypeId.ToString()
                }),
               


            };



            if (id == null || id == 0)
            {
                //create product
                //ViewBag.EmployeeList = EmployeeList;
                //ViewBag.CoverTypeList = CoverTypeList;

                if (!User.IsInRole(SD.Role_Admin))
                {
                    testMethod();



                }



                return View(requestLeaveVM);
            }
            else
            {
                //update product
                requestLeaveVM.RequestLeave = _unitOfWork.RequestLeave.GetFirstOrDefault(u => u.LeaveRequestId == id);
                return View(requestLeaveVM);

            }



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(RequestLeaveVM obj)
        {
            if (ModelState.IsValid)
            {

                string msg = "";


                if (obj.RequestLeave.LeaveRequestId == 0)
                {
                    var employeeFromDb =  _unitOfWork.EmployeeLeave.GetFirstOrDefault(u => u.Id == obj.RequestLeave.EmployeeLeaveId);
                    int leaaveTypeId = obj.RequestLeave.LeaveTypeId;

                    var leaveTypeFromDb = _unitOfWork.LeaveType.GetFirstOrDefault(u => u.LeaveTypeId == leaaveTypeId);
                    string leaveType = leaveTypeFromDb.LeaveTypeName;

                    //remain Days
                    int remainCasualLeaves =(int)(employeeFromDb.CasualLeaves - employeeFromDb.GetCasualLeaves);
                    int remainAnnualLeaves =(int)(employeeFromDb.AnnualLeaves - employeeFromDb.GetAnnualLeaves);
                    int remainMedicalLeaves =(int)(employeeFromDb.MedicalLeaves - employeeFromDb.GetMedicalLeaves);



                    if (leaveType == "Casual" )
                    {
                        if (obj.RequestLeave.Days <= remainCasualLeaves)
                        {
                            if (employeeFromDb.GetCasualLeaves <= employeeFromDb.CasualLeaves)
                            {

                                //ADD
                                addLeave(obj);
								return RedirectToAction("Index");
							}
                            else
                            {
								//ERROR
								ModelState.AddModelError("requestLeave.LeaveTypeId", "Casual Leaves are exeeded please try other one");
							}
                        }
                        else
                        {
							//ERROR
							ModelState.AddModelError("requestLeave.Days", "Selected Days are bigger than remain days");
						}

							
                    }
                    else if (leaveType == "Annual")
					{
						if ( obj.RequestLeave.Days <= remainAnnualLeaves)
						{
							if (employeeFromDb.GetAnnualLeaves <= employeeFromDb.AnnualLeaves)
							{

								//ADD
								addLeave(obj);
								return RedirectToAction("Index");
							}
							else
							{
								//ERROR
								ModelState.AddModelError("requestLeave.LeaveTypeId", "Annual Leaves are exeeded please try other one");
							}
						}
						else
						{
							//ERROR
							ModelState.AddModelError("requestLeave.Days", "Selected Days are bigger than remain days");
						}
					}
                    else 
                    {
						if ( obj.RequestLeave.Days <= remainMedicalLeaves)
						{
							if (employeeFromDb.GetMedicalLeaves <= employeeFromDb.MedicalLeaves)
							{

								//ADD
								addLeave(obj);
								return RedirectToAction("Index");
							}
							else
							{
								//ERROR
								ModelState.AddModelError("requestLeave.LeaveTypeId", "Medical Leaves are exeeded please try other one");
							}
						}
						else
						{
							//ERROR
							ModelState.AddModelError("requestLeave.Days", "Selected Days are bigger than remain days");
						}
					}
                    

                    
                }
                else
                {
                    _unitOfWork.RequestLeave.Update(obj.RequestLeave);
                    msg = "Updated";
                }

                
            }
			RequestLeaveVM requestLeaveVM = new()

			{
				RequestLeave = new(),

				LeaveTypeList = _unitOfWork.LeaveType.GetAll().Select(i => new SelectListItem
				{
					Text = i.LeaveTypeName,
					Value = i.LeaveTypeId.ToString()
				}),



			};

			if (!User.IsInRole(SD.Role_Admin))
            {
                


                testMethod();
            }

                return View(requestLeaveVM);


        }


        public void testMethod()
        {
            ApplicationUser applicationUser = new();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId);

            string fullName = applicationUser.FullName;
            string userCode = applicationUser.UserCode;

            var employeeFromDb = _unitOfWork.EmployeeLeave.GetFirstOrDefault(u => u.UserId == userCode);

            int annualLeaves = (int)(employeeFromDb.AnnualLeaves - employeeFromDb.GetAnnualLeaves);
            int casualLeaves = (int)(employeeFromDb.CasualLeaves - employeeFromDb.GetCasualLeaves);
            int medicalLeaves = (int)(employeeFromDb.MedicalLeaves - employeeFromDb.GetMedicalLeaves);
            int employeeId = employeeFromDb.Id;

            ViewData["annualLeaves"] = annualLeaves;
            ViewData["casuallLeaves"] = casualLeaves;
            ViewData["medicalLeaves"] = medicalLeaves;
            ViewData["employeeId"] = employeeId;
        }

        public void addLeave(RequestLeaveVM obj )
        {
			_unitOfWork.RequestLeave.Add(obj.RequestLeave);
			_unitOfWork.Save();
			TempData["success"] = "Leave Request Added Successfully";
			
		}


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            if (!User.IsInRole(SD.Role_Admin))
            {
                ApplicationUser applicationUser = new();

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId);

                string fullName = applicationUser.FullName;
                string userCode = applicationUser.UserCode;

                var employeeFromDb = _unitOfWork.EmployeeLeave.GetFirstOrDefault(u => u.UserId == userCode);
                int id =  employeeFromDb.Id;


                var userLeaves = _unitOfWork.RequestLeave.GetById(id, includeProperties: "LeaveType,EmployeeLeave");

                
                //var leaveType = _unitOfWork.LeaveType.GetFirstOrDefault(u => u.LeaveTypeId == leaveTypeId);

                return Json(new { data = userLeaves });


            }
            else
            {
                var leaveRequestList = _unitOfWork.RequestLeave.GetAll(includeProperties: "LeaveType,EmployeeLeave");
                return Json(new { data = leaveRequestList });
            }


                

        }

        [HttpDelete]
        public IActionResult Delete(int? id)

        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var employeeFromDb = _db.Employees.Find(id);
            var obj = _unitOfWork.RequestLeave.GetFirstOrDefault(u => u.LeaveRequestId == id);


            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });

            }


            _unitOfWork.RequestLeave.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successfull" });
            // return RedirectToAction("Index");
        }


        [Authorize(Roles = SD.Role_Admin)]

        [HttpPut]
        public IActionResult Decline(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            

            var obj = _unitOfWork.RequestLeave.GetFirstOrDefault(u => u.LeaveRequestId == id);

            if (obj == null)
            {
                return Json(new { success = false, message = "Error while Declined" });

            }

            _unitOfWork.RequestLeave.Decline(obj);
            
            _unitOfWork.Save();

            return Json(new { success = true, message = "Declined Successfull" });
            


        }


        [Authorize(Roles = SD.Role_Admin)]
        [HttpPut]
        public IActionResult Approve(int? id)

        {
            if (id == null || id == 0)
            {
                return NotFound();
            }



            var obj = _unitOfWork.RequestLeave.GetFirstOrDefault(u => u.LeaveRequestId == id);

            int leaveTypeId = obj.LeaveTypeId;
            int employeeId = obj.EmployeeLeaveId;

            var employeeTypeFromDb =  _unitOfWork.EmployeeLeave.GetFirstOrDefault(u => u.Id == employeeId);


            if (obj == null)
            {
                return Json(new { success = false, message = "Error while Declined" });

            }

            _unitOfWork.RequestLeave.Approve(obj,employeeTypeFromDb);


            _unitOfWork.Save();

            return Json(new { success = true, message = "Approve Successfull" });



        }

        #endregion

    }
}
