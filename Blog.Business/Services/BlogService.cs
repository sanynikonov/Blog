﻿using AutoMapper;
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

        public async Task Create(BlogModel model, int userId)
        {
            var user = await unit.UserRepository.GetByIdAndIncludeAsync(userId, x => x.Blogs);

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
                var activeUsers = unit.UserManager.Users.Where(user => blog.Posts.Any(post => post.AuthorId != null && post.AuthorId == user.Id)).ToList();

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
            var includes = new Dictionary<Expression<Func<Data.Blog, object>>, Expression<Func<object, object>>[]>
            {
                { x => x.Posts, new Expression<Func<object, object>>[] { x => ((Post)x).Author } }
            };

            var blog = await unit.BlogRepository.GetByIdAndIncludeAsync(id, includes);

            return mapper.Map<BlogModel>(blog);
        }
    }
}
