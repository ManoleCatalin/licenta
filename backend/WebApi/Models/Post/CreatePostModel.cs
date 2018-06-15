using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class CreatePostModel
    {
        public string Title { get; set; }
        public string SourceUrl { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Guid> Interests { get; set; }
    }
}
