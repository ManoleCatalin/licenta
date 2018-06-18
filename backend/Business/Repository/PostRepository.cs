using Core.Domain;
using Core.Interfaces;
using Core.Ordering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Business.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context)
        {
        }

        override public IEnumerable<Post> Get(int pageIndex, int pageSize, Ordering<Post> ordering = null)
        {
            var entities = _entities.AsQueryable();

            if (ordering != null)
            {
                entities = ordering.Apply(entities);
            }

            return entities
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Include(x => x.PostInterests)
                    .ThenInclude(y => y.Interest)
                .ToList();
        }
    }
}
