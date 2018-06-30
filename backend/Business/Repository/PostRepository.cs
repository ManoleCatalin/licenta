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

            var userInterests = user.UserInterest;


            var entities = _entities.AsQueryable();

            if (ordering != null)
            {
                entities = ordering.Apply(entities);
            }

            return (entities.AsEnumerable()).Where(p =>
            {

                if (selfPosts)
                {
                    if (p.UserId == userId) return true;
                }
                else
                {
                    foreach (var postInterest in p.PostInterests)
                    {
                        foreach (var userInterest in userInterests)
                        {
                            if (userInterest.InterestId == postInterest.InterestId)
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize).ToList();
        }

        
    }
}
