using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Business;
using Blog.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Blog.Presentation.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IPostService postService;

        public BlogsController(IBlogService blogService, IPostService postService)
        {
            this.blogService = blogService;
            this.postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await blogService.GetAll());
        }

        public async Task<IActionResult> Details(int id)
        {
            var blog = await blogService.GetById(id);

            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostInBlog(PostFormViewModel model)
        {
            var post = new PostModel { Name = model.Name, Content = model.Content, AuthorId = 1 };

            await postService.AddToBlog(post, model.BlogId);

            return RedirectToAction(nameof(BlogsController.Details), "Blogs", new { id = model.BlogId });
        }

        [HttpGet]
        public IActionResult CreatePostInBlog(int blogId)
        {
            return View(new PostFormViewModel { BlogId = blogId });
        }
    }
}