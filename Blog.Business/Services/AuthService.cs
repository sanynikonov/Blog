using Blog.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unit;

        public AuthService(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public string GetUserId(ClaimsPrincipal principal)
        {
            return unit.UserManager.GetUserId(principal);
        }

        public async Task<SignInResult> Login(LoginModel model)
        {
            return await unit.SignInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        }

        public async Task Logout()
        {
            await unit.SignInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            return await unit.UserManager.CreateAsync(user, model.Password);
        }
    }
}
