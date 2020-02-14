using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IgnProtoView.Data
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IgniteUser>
    {
        private UserManager<IgniteUser> _userManager;
        public MyUserClaimsPrincipalFactory(
            UserManager<IgniteUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IgniteUser user)
        {
            IEnumerable<Claim> ct = new List<Claim>
            {

            };
            ClaimsIdentity identity = await base.GenerateClaimsAsync(user);


            // if user is of specific Ignite Type Role
            var role = await _userManager.GetRolesAsync(user);

            identity.AddClaim(new Claim("UserName", user.FirstName ?? "New Ignite User"));
            identity.AddClaim(new Claim("LastName", user.LastName ?? "New Ignite User"));
            identity.AddClaim(new Claim("UserType", user.FkIgniteUserTypeId.ToString() ?? "New Ignite User"));

            if (role.Contains(Utility.UserRole.AdminUser))
            {
                identity.AddClaim(new Claim("Role", Utility.UserRole.AdminUser ?? "New Ignite User"));
            }
            if (role.Contains(Utility.UserRole.HR))
            {
                identity.AddClaim(new Claim("Role", Utility.UserRole.HR ?? "New Ignite User"));
            }
            if (role.Contains(Utility.UserRole.ManagerUser))
            {
                identity.AddClaim(new Claim("Role", Utility.UserRole.ManagerUser ?? "New Ignite User"));
            }
            if (role.Contains(Utility.UserRole.RegEmp))
            {
                identity.AddClaim(new Claim("Role", Utility.UserRole.RegEmp ?? "New Ignite User"));
            }
            return identity;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(IgniteUser user)
        {
            var principal = await base.CreateAsync(user);
            //var appUser = UserManager.Ge

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim(ClaimTypes.GivenName, user.FirstName)
                });
            }
            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                     new Claim(ClaimTypes.Surname, user.LastName),
                });
            }

            //if (!string.IsNullOrWhiteSpace(user.LastName))
            //{
            //    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            //         new Claim(ClaimTypes.Role, user.Roles.Where(x => x.)),
            //    });
            //}
            return principal;
        }
    }
}
