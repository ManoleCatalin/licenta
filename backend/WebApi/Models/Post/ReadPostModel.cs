using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Post
{
    public class ReadPostModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SourceUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public ICollection<ReadInterest> Interests { get; set; }
    }
}
