using Blog.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Presentation.Models
{
    public class PostSearchViewModel
    {
        public string SearchString { get; set; }
        public string SearchByType { get; set; }
        public IEnumerable<PostModel> Posts { get; set; }

        public PostSearchViewModel()
        {
            Posts = new List<PostModel>();
        }
    }
}
