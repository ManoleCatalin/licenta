﻿using Core.Interfaces;
using System;

namespace Core.Domain
{
    public class Like : ISoftDeletable
    {
       public  Guid Id { get; set; }
       public User User { get; set; }
       public Post Post { get; set; }
       public DateTime CreatedAt { get; set; }
    }
}