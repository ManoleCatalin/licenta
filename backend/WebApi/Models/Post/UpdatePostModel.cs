using System;
using System.Collections.Generic;

namespace WebApi.Models.Post
{
    public class UpdatePostModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SourceUrl { get; set; }
        public string Description { get; set; }
        public ICollection<ReadInterest> Interests { get; set; }
    }
}
