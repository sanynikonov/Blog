using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogContext context;

        public UnitOfWork(BlogContext context, IRepository<User> userRepository, IRepository<Blog> blogRepository, IRepository<Post> postRepository)
        {
            this.context = context;
            UserRepository = userRepository;
            BlogRepository = blogRepository;
            PostRepository = postRepository;
        }

        public IRepository<User> UserRepository { get; }

        public IRepository<Blog> BlogRepository { get; }

        public IRepository<Post> PostRepository { get; }



        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
