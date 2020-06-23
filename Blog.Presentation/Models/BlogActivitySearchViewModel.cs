using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Business;

namespace Blog.Presentation.Models
{
    public class BlogActivitySearchViewModel
    {
        public DateTime Oldest { get; set; }
        public DateTime Latest { get; set; }
        public IEnumerable<BlogActivityInfoModel> Blogs { get; set; }

        public BlogActivitySearchViewModel()
        {
            Blogs = new List<BlogActivityInfoModel>();
        }
    }
}
