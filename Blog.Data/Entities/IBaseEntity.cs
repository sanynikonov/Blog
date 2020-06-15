using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data
{
    public interface IBaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
