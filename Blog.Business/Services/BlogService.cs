using AutoMapper;
using Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Blog.Business
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

        public async Task Create(BlogModel model, string userId)
        {
            var user = await unit.UserManager.FindByIdAsync(userId);

            if (user == null)
                throw new InvalidOperationException($"User with id {userId} does not exist.");

            var blog = mapper.Map<Data.Blog>(model);

            user.Blogs.Add(blog);

            await unit.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogListItemModel>> GetAll()
        {
            var blogs = await unit.BlogRepository.GetAsync(x => true);

            return mapper.Map<IEnumerable<BlogListItemModel>>(blogs);
        }

        public async Task<IEnumerable<BlogActivityInfoModel>> GetByBiggestActivityInPeriod(DateTime oldest, DateTime latest)
        {
            var blogs = await unit.BlogRepository.GetAndIncludeAsync(
                predicate: x => x.Posts.Any(x => oldest < x.Publication && x.Publication < latest),
                x => x.Posts, x => x.Author);

            var models = new List<BlogActivityInfoModel>();

            foreach (var blog in blogs)
            {
                var postsAuthorIds = blog.Posts.Select(x => x.AuthorId);

                var activeUsersCount = unit.UserManager.Users.Count(user => postsAuthorIds.Any(authorId => authorId == user.Id));

                var model = new BlogActivityInfoModel
                {
                    Blog = mapper.Map<BlogListItemModel>(blog),
                    PostsCount = blog.Posts.Count(),
                    UsersCount = activeUsersCount
                };

                models.Add(model);
            }

            return models.OrderByDescending(x => x.PostsCount + x.UsersCount);
        }

        public async Task<BlogModel> GetById(int id)
        {
            var includes = new Dictionary<Expression<Func<Data.Blog, object>>, Expression<Func<object, object>>[]>
            {
                { x => x.Posts, new Expression<Func<object, object>>[] { x => ((Post)x).Author } }
            };

            var blog = await unit.BlogRepository.GetByIdAndIncludeAsync(id, includes);

            return mapper.Map<BlogModel>(blog);
        }

        public async Task<IEnumerable<BlogListItemModel>> GetByUserId(string userId)
        {
            var user = await unit.UserRepository.GetByIdAndIncludeAsync(userId, p => p.Blogs);

            return mapper.Map<IEnumerable<BlogListItemModel>>(user.Blogs);
        }
    }
}
