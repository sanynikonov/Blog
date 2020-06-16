using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Business
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<BlogListItemModel> Blogs { get; set; }

        public UserModel()
        {
            Blogs = new List<BlogListItemModel>();
        }
    }
}
