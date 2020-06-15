using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data
{
    public class Post : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public DateTime Publication { get; set; }

        public int? BlogId { get; set; }
        public Blog Blog { get; set; }

        public string AuthorId { get; set; }
        public User Author { get; set; }
    }
}
