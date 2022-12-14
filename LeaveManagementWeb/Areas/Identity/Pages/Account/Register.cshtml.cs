// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using LeaveManagement.DataAccess.Repository.IRepository;
using LeaveManagement.Models;
using LeaveManagement.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace LeaveManagementWeb.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IUnitOfWork unitOfWork,
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string FullName { get; set; }
            public string UserCode { get; set; }
            public string PhoneNumber { get; set; }

            public string? Role { get; set; } 

            public int? EmployeeTypeId { get; set; }


            [ValidateNever]
            public IEnumerable<SelectListItem> RoleList { get; set; }

            [ValidateNever]
            public IEnumerable<SelectListItem> EmployeTypes { get; set; }


        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
            }

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            Input = new InputModel()
            {
                RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                }),

                EmployeTypes = _unitOfWork.EmployeeType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.EmployeeTypeName,
                    Value = i.EmployeeTypeId.ToString()
                }),

            };
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                //ApplicationUser applicationUser = new();
                //string name = applicationUser.FullName;

                string lastUserId = _unitOfWork.ApplicationUser.GetLastUserId();

               
                string userCode = "";
                string uLeteer = "U";
                if (lastUserId == "U0000" || lastUserId == null)
                {
                    userCode = uLeteer+"0001";
                }else
                {
                    int newValue = Int32.Parse(lastUserId.Substring(1,4));
                    int newId = newValue + 1;

                    if (newId < 10)
                    {
                        userCode = uLeteer + "000" + newId;
                    }else if(newId > 10)
                    {
                        userCode = uLeteer + "00" + newId;
                    }
                    else if (newId > 100)
                    {
                        userCode = uLeteer + "0" + newId;
                    }
                    else
                    {
                        userCode = uLeteer + "0" + newId;
                    }
                }


                //create table

                if (Input.Role == SD.Role_Employee)
                {
                    //LeaveType leaveType = new();

                    //leaveType.LeaveTypeName = "Malith";
                    //leaveType.TotalLeaves = 10;

                    //_unitOfWork.LeaveType.Add(leaveType);

                    int annualLeaves;
                    int casualLeaves;
                    int medicalLeaves;

                    var leavesFromDb = _unitOfWork.EmployeeType.GetFirstOrDefault(u => u.EmployeeTypeId == Input.EmployeeTypeId);


                    if (leavesFromDb == null)
                    {
                        annualLeaves = 0;
                        casualLeaves = 0;
                        medicalLeaves = 0;

                    }
                    else
                    {
                        annualLeaves = (int)leavesFromDb.AnnualLeaves;
                        casualLeaves = (int)leavesFromDb.CasualLeaves;
                        medicalLeaves = (int)leavesFromDb.MedicalLeaves;
                    }




                    EmployeeLeave employeeLeave = new();

                    employeeLeave.AnnualLeaves = annualLeaves;
                    employeeLeave.CasualLeaves = casualLeaves;
                    employeeLeave.MedicalLeaves = medicalLeaves;
                    employeeLeave.GetAnnualLeaves = 0;
                    employeeLeave.GetCasualLeaves = 0;
                    employeeLeave.GetMedicalLeaves = 0;
                    employeeLeave.UserId = userCode;
                    employeeLeave.EmployeeTypeId = (int)Input.EmployeeTypeId;

					var userId = await _userManager.GetUserIdAsync(user);
                   
                    employeeLeave.UserCode = userId;


					_unitOfWork.EmployeeLeave.Add(employeeLeave);
                }



                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                user.PhoneNumber = Input.PhoneNumber;
                user.FullName = Input.FullName;
                user.UserCode = userCode;
                if(Input.Role == SD.Role_Employee)
                {
                    user.EmployeeTypeId= Input.EmployeeTypeId;
                }

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if(Input.Role == null)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Employee);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, Input.Role);
                    }


                    


                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
