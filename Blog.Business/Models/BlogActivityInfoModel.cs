using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Business
{
    public class BlogActivityInfoModel
    {
        public int Id { get; set; }
        public BlogListItemModel Blog { get; set; }
        public int PostsCount { get; set; }
        public int UsersCount { get; set; }

    }
}
