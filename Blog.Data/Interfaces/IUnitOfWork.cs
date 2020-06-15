using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public interface IUnitOfWork
    {
        IRepository<int, Blog> BlogRepository { get; }
        IRepository<int, Post> PostRepository { get; }
        IRepository<string, User> UserRepository { get; }
        UserManager<User> UserManager { get; }
        SignInManager<User> SignInManager { get; }

        Task SaveChangesAsync();
    }
}
