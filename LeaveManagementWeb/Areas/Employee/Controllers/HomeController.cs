
using LeaveManagement.DataAccess.Repository;
using LeaveManagement.DataAccess.Repository.IRepository;
using LeaveManagement.Models;
using LeaveManagementWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LeaveManagementWeb.Areas.Employee.Controllers;
[Area("Employee")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        ApplicationUser applicationUser = new();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return View(applicationUser);
        }

        applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId);

        return View(applicationUser);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}