using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Blog> BlogRepository { get; }
        IRepository<Post> PostRepository { get; }

        Task SaveChangesAsync();
    }
}
