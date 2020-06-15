using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data
{
    public class User : IdentityUser, IBaseEntity<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Post> Posts { get; set; }

        public User()
        {
            Blogs = new List<Blog>();
            Posts = new List<Post>();
        }
    }
}
