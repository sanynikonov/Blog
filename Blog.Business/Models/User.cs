using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Business
{
    public class UserModel
    {
        public string FirstName { get; set; } = "Ivan";
        public string LastName { get; set; } = "Ivanenko";

        public ICollection<BlogModel> Blogs { get; set; }
        public ICollection<PostModel> Posts { get; set; }

        public UserModel()
        {
            Blogs = new List<BlogModel>();
            Posts = new List<PostModel>();
        }
    }
}
