using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Business
{
    public class BlogModel
    {
        public string Name { get; set; }

        public ICollection<PostModel> Posts { get; set; }

        public BlogModel()
        {
            Posts = new List<PostModel>();
        }
    }
}
