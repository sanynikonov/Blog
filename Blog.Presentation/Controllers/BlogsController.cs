using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Business;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogService blogService;

        public BlogsController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await blogService.GetAll());
        }
    }
}