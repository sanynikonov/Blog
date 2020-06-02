using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Business
{
    public class PostModel
    {
        public string Name { get; set; }
        public string Content { get; set; }

        public DateTime Publication { get; set; }

        public int? AuthorId { get; set; }
        public UserModel Author { get; set; }
    }
}
