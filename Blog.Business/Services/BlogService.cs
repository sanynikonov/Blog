using AutoMapper;
using Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public BlogService(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task Create(BlogModel model, int userId)
        {
            var user = await unit.UserRepository.GetByIdAndIncludeAsync(userId, x => x.Blogs);

            if (user == null)
                throw new InvalidOperationException($"Blog with id {userId} does not exist.");

            var blog = mapper.Map<Data.Blog>(model);

            user.Blogs.Add(blog);

            await unit.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogActivityInfoModel>> GetByBiggestActivityInPeriod(DateTime oldest, DateTime latest)
        {
            var blogs = await unit.BlogRepository.GetAndIncludeAsync(
                predicate: x => x.Posts.Any(x => oldest < x.Publication && x.Publication < latest),
                x => x.Posts, x => x.Author);

            var models = new List<BlogActivityInfoModel>();

            foreach (var blog in blogs)
            {
                var activeUsers = await unit.UserRepository.GetAsync(user => blog.Posts.Any(post => post.AuthorId != null && post.AuthorId == user.Id));

                var model = new BlogActivityInfoModel
                {
                    Blog = mapper.Map<BlogListItemModel>(blog),
                    PostsCount = blog.Posts.Count(),
                    UsersCount = activeUsers.Count()
                };

                models.Add(model);
            }

            return models;
        }

        public async Task<BlogModel> GetById(int id)
        {
            var blog = await unit.BlogRepository.GetByIdAsync(id);

            return mapper.Map<BlogModel>(blog);
        }
    }
}
