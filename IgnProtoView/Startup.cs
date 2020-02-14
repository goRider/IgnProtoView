using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IgnProtoView.Data;
using IgnProtoView.Email;
using IgnProtoView.Views.Account.InputModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IgnProtoView
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<IgniteContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IgniteAppData")));

            services.AddIdentity<IgniteUser, IgniteRole>(options =>
                {
                    options.User.RequireUniqueEmail = false;
                })
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<IgniteContext>()
                .AddClaimsPrincipalFactory<MyUserClaimsPrincipalFactory>();

            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<EmailOptions>(Configuration);

            services.AddSingleton<IgniteUser>();

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Views/Account/Login";
                option.LogoutPath = "/Views/Account/Logout";
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAccess", policy => policy.RequireRole("Admin"));

                options.AddPolicy("ManagerAccess", policy => policy.RequireAssertion(context => context.User.IsInRole(Data.Utility.UserRole.AdminUser) || context.User.IsInRole(Data.Utility.UserRole.ManagerUser)));

                options.AddPolicy("User Access", policy => policy.RequireAssertion(context => context.User.IsInRole(Data.Utility.UserRole.HR) || context.User.IsInRole(Data.Utility.UserRole.AdminUser) || context.User.IsInRole(Data.Utility.UserRole.ManagerUser)));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHttpContextAccessor();

            services.TryAddScoped<IPasswordValidator<IgniteUser>, PasswordValidator<IgniteUser>>();
            services.TryAddScoped<IPasswordHasher<IgniteUser>, PasswordHasher<IgniteUser>>();

            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<IgniteUser>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<IgniteUser>, UserClaimsPrincipalFactory<IgniteUser>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseIdentity();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
