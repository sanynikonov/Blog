using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business
{
    public interface IPostService
    {
        Task<IEnumerable<PostModel>> GetByPartialContent(string content);
        Task<IEnumerable<PostModel>> GetByName(string name);
        Task AddToBlog(PostModel model, int blogId);
    }
}
