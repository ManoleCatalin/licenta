using Core.Domain;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Business.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public override User Get(Guid id)
        {
            return _entities.Include(x => x.UserInterest).Where(x=> x.Id == id).FirstOrDefault();
        }
    }
}
