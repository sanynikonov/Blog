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

        public async Task<IActionResult> Index(string searchByType, string searchString)
        {
            var model = new PostSearchViewModel();

            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(searchByType))
            {
                if (searchByType == "Name")
                    model.Posts = await postService.GetByName(searchString);

                else if (searchByType == "Content")
                    model.Posts = await postService.GetByPartialContent(searchString);

                else
                    return BadRequest($"Search type {searchByType} not found.");
            }

            return View(model);
        }
    }
}