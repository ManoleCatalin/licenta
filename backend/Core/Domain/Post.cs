﻿using Core.Interfaces;
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
        public DateTime CreateAt { get; set; }
        public User Author { get; set; }
        public ICollection<Interest> Interests { get; set; }
    }
}
