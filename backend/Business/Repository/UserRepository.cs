using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
