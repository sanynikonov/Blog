using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business
{
    public interface IAuthService
    {
        Task<SignInResult> Login(LoginModel model);
        Task<IdentityResult> Register(RegisterModel model);
        string GetUserId(ClaimsPrincipal principal);
        Task Logout();
    }
}
