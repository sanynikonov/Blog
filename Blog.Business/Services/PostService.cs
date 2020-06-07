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
            var blog = await unit.BlogRepository.GetByIdAsync(blogId);

            if (blog == null)
                throw new ArgumentException($"Blog with id {blogId} does not exist.");

            if (model.AuthorId == null)
                throw new ArgumentNullException($"Author id was null.");

            var author = await unit.UserRepository.GetByIdAsync(model.AuthorId.Value);

            if (author == null)
                throw new ArgumentException($"User with id {model.AuthorId.Value} does not exist.");

            var post = mapper.Map<Post>(model);

            post.BlogId = blog.Id;

            blog.Posts.Add(post);

            await unit.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostModel>> GetByName(string name)
        {
            var posts = await unit.PostRepository.GetAsync(
                x => x.Name.Contains(name));

            return mapper.Map<IEnumerable<PostModel>>(posts);
        }

        public async Task<IEnumerable<PostModel>> GetByPartialContent(string content)
        {
            var posts = await unit.PostRepository.GetAsync(
                x => x.Content.Contains(content));

            return mapper.Map<IEnumerable<PostModel>>(posts);
        }
    }
}
