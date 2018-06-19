using System;
using System.Collections.Generic;

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
        public ICollection<ReadInterestModel> Interests { get; set; }
        public Guid? LikeId { get; set; }
        public Guid? FavoriteId { get; set; }
    }
}
