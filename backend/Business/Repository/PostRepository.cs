using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using Core.Interfaces;
using Core.Ordering;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context)
        {
        }

        public int GetLikesCountOfPost(Guid postId)
        {
           return _entities.First(x => x.Id == postId).Likes.Count();
        }

        public IEnumerable<Post> GetPostsForInterest(Guid interestId, int pageIndex = 1, int pageSize = 1)
        {
            return _entities.Where(x => 1 == x.PostInterests.Count(y => y.InterestId == interestId))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }

        public virtual IEnumerable<Post> Get(Guid userId, bool selfPosts, int pageIndex = 1, int pageSize = 1, Ordering<Post> ordering = null)
        {
            var user = Context.Set<User>().Where(x=> x.Id == userId).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            var entities = _entities.AsQueryable();

            if(selfPosts)
            {
                entities = entities.Where(p => p.UserId == userId);
            }
            else
            {
                var userInterests = Context.Set<UserInterest>().Where(u => u.UserId == userId);
                entities = entities.Where(p => 0 != p.PostInterests.Count(x => 0 != userInterests.Count(u => u.InterestId == x.InterestId)));
            }


            if (ordering != null)
            {
                entities = ordering.Apply(entities);
            }

            return entities
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
        }

        public IEnumerable<Post> GetFavoritePosts(Guid userId, int pageIndex = 1, int pageSize = 1)
        {
            return _entities.Where(p => 0 != p.Favorites.Count(f => userId == f.UserId))
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
        }
    }
}
