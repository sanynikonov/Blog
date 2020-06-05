using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Presentation.Models
{
    public class PostFormViewModel
    {
        [Required]
        public int BlogId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
