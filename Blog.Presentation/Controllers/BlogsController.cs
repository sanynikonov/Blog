using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Business;
using Blog.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Blog.Presentation.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IPostService postService;
        private readonly IUserService userService;

        public BlogsController(IBlogService blogService, IPostService postService, IUserService userService)
        {
            this.blogService = blogService;
            this.postService = postService;
            this.userService = userService;
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
            var userId = userService.GetUserId(User);

            var post = new PostModel { Name = model.Name, Content = model.Content, AuthorId = userId };

            await postService.AddToBlog(post, model.BlogId);

            return RedirectToAction(nameof(BlogsController.Details), "Blogs", new { id = model.BlogId });
        }

        [HttpGet]
        public IActionResult CreatePostInBlog(int blogId)
        {
            return View(new PostFormViewModel { BlogId = blogId });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BlogModel model)
        {
            var userId = userService.GetUserId(User);

            await blogService.Create(model, userId);

            return RedirectToAction("GetByUser");
        }

        [Authorize]
        public async Task<IActionResult> GetByUser()
        {
            var userId = userService.GetUserId(User);

            return View("Index", await blogService.GetByUserId(userId));
        }
    }
}