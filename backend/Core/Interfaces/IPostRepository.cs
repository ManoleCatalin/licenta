using Core.Domain;
using Core.Ordering;
using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetPostsForInterest(Guid interestId, int pageIndex = 1, int pageSize = 1);
        IEnumerable<Post> GetFavoritePosts(Guid userId, int pageIndex = 1, int pageSize = 1);
        IEnumerable<Post> Get(Guid userId, bool selfPosts, int pageIndex = 1, int pageSize = 1, Ordering<Post> ordering = null);
        int GetLikesCountOfPost(Guid post);
    }
}
