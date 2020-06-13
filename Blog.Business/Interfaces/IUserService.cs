using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business
{
    public interface IUserService
    {
        Task<SignInResult> Login(LoginModel model);
        Task<IdentityResult> Register(RegisterModel model);
    }
}
