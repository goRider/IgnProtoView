using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IgnProtoView.Data;
using IgnProtoView.Views.Account.InputModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IgnProtoView.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        private readonly SignInManager<IgniteUser> _signInManager;
        private readonly UserManager<IgniteUser> _userManager;
        private IgniteContext _context;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<LoginViewComponent> _logger;
        private readonly RoleManager<IgniteRole> _roleManager;

        public LoginViewComponent(SignInManager<IgniteUser> signInManager, UserManager<IgniteUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public LoginInputModel Input { get; set; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            if (_signInManager.IsSignedIn(this.User as ClaimsPrincipal))
            {
                return View("~/Views/Home/Index.cshtml", await _userManager.GetUserAsync(this.User as ClaimsPrincipal));
            }

            return View("~/Views/Account/SignedOut.cshtml");
        }
    }
}
