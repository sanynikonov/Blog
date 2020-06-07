using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogContext context;

        public UnitOfWork(BlogContext context, IRepository<Blog> blogRepository, 
            IRepository<Post> postRepository, IRepository<User> userRepository, 
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.context = context;
            BlogRepository = blogRepository;
            PostRepository = postRepository;
            UserRepository = userRepository;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public IRepository<Blog> BlogRepository { get; }

        public IRepository<Post> PostRepository { get; }

        public IRepository<User> UserRepository { get; }

        public UserManager<User> UserManager { get; }

        public SignInManager<User> SignInManager { get; }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
