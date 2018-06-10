using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public class User : ISoftDeletable
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
