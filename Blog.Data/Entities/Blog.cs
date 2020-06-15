using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data
{
    public class Blog : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string AuthorId { get; set; }
        public User Author { get; set; }
        public ICollection<Post> Posts { get; set; }

        public Blog()
        {
            Posts = new List<Post>();
        }
    }
}
