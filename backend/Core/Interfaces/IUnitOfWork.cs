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
        IFavoriteRepository Favorites { get; }

        IEnumerable<Post> GetPostsForUser(Guid userId, bool selfPosts, int page = 1, int pageSize = 1, Ordering<Post> ordering = null, Guid? interestId = null);
        int Complete();
    }
}
