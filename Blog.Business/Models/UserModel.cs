using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Business
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "Ivan";
        public string LastName { get; set; } = "Ivanenko";

        public ICollection<BlogListItemModel> Blogs { get; set; }

        public UserModel()
        {
            Blogs = new List<BlogListItemModel>();
        }
    }
}
