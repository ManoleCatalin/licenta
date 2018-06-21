using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Post
{
    public class EditPostModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SourceUrl { get; set; }
        public string Description { get; set; }
        public ICollection<Guid> Interests { get; set; }
    }
}
