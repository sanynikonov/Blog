using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data
{
    public class User : BaseEntity
    {
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Post> Posts { get; set; }

        public User()
        {
            Blogs = new List<Blog>();
            Posts = new List<Post>();
        }
    }
}
