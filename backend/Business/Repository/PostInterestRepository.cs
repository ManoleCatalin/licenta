using Core.Domain;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class PostInterestRepository : Repository<PostInterest>, IPostInterestRepository
    {
        public PostInterestRepository(DbContext context) : base(context)
        {
        }
    }
}
