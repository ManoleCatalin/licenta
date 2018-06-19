using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public class Post : ISoftDeletable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SourceUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        virtual public User User { get; set; }
        virtual public ICollection<PostInterest> PostInterests { get; set; }
        virtual public ICollection<Favorite> Favorites { get; set; }
        virtual public ICollection<Like> Likes { get; set; }
    }
}
