using Core.Domain;
using Core.Ordering;
using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IPostRepository Posts { get; }
        IInterestRepository Interests { get; }
        IPostInterestRepository PostInterests { get; }
        IRepository<UserInterest> UserInterests { get; }
        ILikeRepository Likes { get; }

        IEnumerable<Post> GetPostsForUser(Guid userId, int page = 1, int pageSize = 1, Ordering<Post> ordering = null);
        int Complete();
    }
}
