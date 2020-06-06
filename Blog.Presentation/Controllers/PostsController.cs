using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Business;
using Blog.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var model = new PostSearchViewModel();

            if (!string.IsNullOrEmpty(searchString))
            {
                model.Posts = await postService.GetByName(searchString);
            }

            return View(model);
        }

        public async Task<IActionResult> Search()
        {
            IEnumerable<PostModel> posts = new List<PostModel>();

            return View(posts);
        }
    }
}