using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context)
        {
        }
    }
}
