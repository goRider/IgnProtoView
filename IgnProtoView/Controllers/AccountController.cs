using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using IgnProtoView.Data;
using IgnProtoView.Data.Utility;
using IgnProtoView.ViewComponents;
using IgnProtoView.Views.Account.InputModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IgnProtoView.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IgniteUser> _signInManager;
        private readonly UserManager<IgniteUser> _userManager;
        private IgniteContext _context;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;
        private readonly RoleManager<IgniteRole> _roleManager;
        private RegisterInputModel Input;
        private readonly IgniteUser _igniteUser;

        public AccountController(SignInManager<IgniteUser> signInManager, UserManager<IgniteUser> userManager, IgniteContext context, IEmailSender emailSender, ILogger<AccountController> logger, RoleManager<IgniteRole> roleManager, IgniteUser igniteUser)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
            _igniteUser = igniteUser;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Register([FromBody]RegisterViewComponent model)
        {
            try
            {
                DateTime? date = null;

                var user = new IgniteUser
                {
                    UserName = model.Input.UserName,
                    NormalizedUserName = model.Input.UserName,
                    Email = model.Input.Email,
                    NormalizedEmail = model.Input.Email,
                    IgniteEmail = model.Input.Email,
                    FirstName = model.Input.FirstName,
                    MiddleInitial = model.Input.MiddleInitial,
                    LastName = model.Input.LastName,
                    FirstNameLastName = string.Format("{0} {1} {2}", model.Input.FirstName, model.Input.MiddleInitial, model.Input.LastName),
                    HiredDate = DateTime.Today,
                    TermDate = date,
                    ApplicationApprovalDate = date
                };

                if (ModelState.IsValid)
                {
                    
                    var nameFormatter = string.Format("{0} {1}", user.FirstName, user.LastName);

                    var claimsIdentity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, nameFormatter),
                    });

                    if (!model.Input.IsAdmin)
                    {
                        user.EmailConfirmed = true;
                    }

                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync(UserRole.AdminUser))
                        {
                            await _roleManager.CreateAsync(new IgniteRole(UserRole.AdminUser));
                        }
                        if (!await _roleManager.RoleExistsAsync(UserRole.HR))
                        {
                            await _roleManager.CreateAsync(new IgniteRole(UserRole.HR));
                        }
                        if (!await _roleManager.RoleExistsAsync(UserRole.ManagerUser))
                        {
                            await _roleManager.CreateAsync(new IgniteRole(UserRole.ManagerUser));
                        }
                        if (!await _roleManager.RoleExistsAsync(UserRole.RegEmp))
                        {
                            await _roleManager.CreateAsync(new IgniteRole(UserRole.RegEmp));
                        }

                        if (Input.IsAdmin)
                        {
                            var i = await _userManager.AddToRoleAsync(user, UserRole.AdminUser);
                        }


                        await _userManager.AddToRoleAsync(user, UserRole.AdminUser);
                        // _logger.LogInformation("User created a new");

                        #region Update Ignite User Types

                        //var claims = new List<Claim>();
                        //{
                        //    claims.Add(new Claim(ClaimTypes.Name, nameFormatter));
                        //    claims.Add(new Claim(ClaimTypes.Role, ""));
                        //};

                        var claims = new List<Claim>();

                        if (await _userManager.IsInRoleAsync(user, UserRole.AdminUser) == true)
                        {
                            user.FkIgniteUserTypeId = 1;
                            claims.Add(new Claim(ClaimTypes.Name, nameFormatter));
                            claims.Add(new Claim(ClaimTypes.Role, UserRole.AdminUser));
                            await _userManager.AddClaimsAsync(user, claims);
                        }
                        if (await _userManager.IsInRoleAsync(user, UserRole.HR) == true)
                        {
                            claims.Add(new Claim(ClaimTypes.Name, nameFormatter));
                            claims.Add(new Claim(ClaimTypes.Role, UserRole.AdminUser));
                            user.FkIgniteUserTypeId = 2;
                        }
                        if (await _userManager.IsInRoleAsync(user, UserRole.ManagerUser) == true)
                        {
                            claims.Add(new Claim(ClaimTypes.Name, nameFormatter));
                            claims.Add(new Claim(ClaimTypes.Role, UserRole.AdminUser));
                            user.FkIgniteUserTypeId = 3;
                        }
                        if (await _userManager.IsInRoleAsync(user, UserRole.RegEmp) == true)
                        {
                            claims.Add(new Claim(ClaimTypes.Name, nameFormatter));
                            claims.Add(new Claim(ClaimTypes.Role, UserRole.AdminUser));
                            user.FkIgniteUserTypeId = 4;
                        }

                        #endregion

                        _logger.LogInformation("User createed a new account with password");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page("/Confirm/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Please confirm email", $"Please confirm your account by <a href='{ HtmlEncoder.Default.Encode(callbackUrl) }'>Click Here</a>.");

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

                        //_context.IgniteUserApplications.Add();

                        await _context.SaveChangesAsync();
                        return View("Login");
                    }
                }
                else
                {
                    return View(model.Input);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return View();
        }

        public async Task<IActionResult> Login([FromBody]LoginViewComponent model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.IgniteUsers.FirstOrDefault(x => x.UserName == model.Input.UserName);
                var result = await _signInManager.PasswordSignInAsync(model.Input.UserName, model.Input.Password, model.Input.RememberMe,
                    lockoutOnFailure: false);
                //var lgoinInfo = 

                if (user != null && !user.EmailConfirmed)
                {
                    return RedirectToPage("VerifyEmail" /*, new { id = model.Input.Email }*/);
                }

                // Login Logic
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in");
                    //return Redirect(returnUrl);
                    await _userManager.FindByEmailAsync(user.Email);
                    var loginInfo = new UserLoginInfo("", "", user.FirstNameLastName);
                    var identity = await _userManager.FindByNameAsync(user.UserName);
                    await _userManager.AddLoginAsync(user, loginInfo);

                    //return RedirectToPage(returnUrl);
                    return View("~/Views/Home/Index.cshtml");
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("~/Views/Home/Index.cshtml",
                        new {ReturnUrl = "", RememberMe = model.Input.RememberMe});
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User Account Locked Out");
                    return RedirectToPage("Lockedout");
                }

                return View("~/Views/Home/Index.cshtml");
            }

            return View();
        }
    }
}
