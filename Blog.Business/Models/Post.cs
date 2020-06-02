using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Business
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public DateTime Publication { get; set; }

        public int? AuthorId { get; set; }
        public PostAuthorInfoModel Author { get; set; }
    }
}
