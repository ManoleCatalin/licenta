using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public class User : IdentityUser<Guid>, ISoftDeletable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        virtual public ICollection<Post> Posts { get; set; }
        virtual public ICollection<Favorite> Favorites { get; set; }
        virtual public ICollection<Like> Likes { get; set; }
        virtual public ICollection<UserInterest> UserInterest { get; set; }
    }
}
