using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data
{
    public class Blog : BaseEntity
    {
        public string Name { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }
        public ICollection<Post> Posts { get; set; }

        public Blog()
        {
            Posts = new List<Post>();
        }
    }
}
