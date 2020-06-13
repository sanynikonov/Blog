using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public interface IUnitOfWork
    {
        IRepository<Blog> BlogRepository { get; }
        IRepository<Post> PostRepository { get; }
        //IRepository<User> UserRepository { get; }
        UserManager<User> UserManager { get; }
        SignInManager<User> SignInManager { get; }

        Task SaveChangesAsync();
    }
}
