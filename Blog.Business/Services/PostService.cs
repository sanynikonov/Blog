using AutoMapper;
using Blog.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public PostService(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task AddToBlog(PostModel model, int blogId)
        {
            var blog = await unit.BlogRepository.GetByIdAndIncludeAsync(blogId, x => x.Posts);

            if (blog == null)
                throw new InvalidOperationException($"Blog with id {blogId} does not exist.");

            var post = mapper.Map<Post>(model);

            blog.Posts.Add(post);

            await unit.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostModel>> GetByName(string name)
        {
            var posts = await unit.PostRepository.GetAsync(
                x => x.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));

            return mapper.Map<IEnumerable<PostModel>>(posts);
        }

        public async Task<IEnumerable<PostModel>> GetByPartialContent(string content)
        {
            var posts = await unit.PostRepository.GetAsync(
                x => x.Name.Contains(content, StringComparison.InvariantCultureIgnoreCase));

            return mapper.Map<IEnumerable<PostModel>>(posts);
        }
    }
}
