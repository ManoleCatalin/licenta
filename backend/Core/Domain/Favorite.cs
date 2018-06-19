using Core.Interfaces;
using System;

namespace Core.Domain
{
    public class Favorite : ISoftDeletable
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        virtual public User User { get; set; }

        public Guid PostId { get; set; }
        virtual public Post Post { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
