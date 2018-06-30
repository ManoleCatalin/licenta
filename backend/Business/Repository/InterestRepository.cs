using Core.Domain;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Repository
{
    class InterestRepository : Repository<Interest>, IInterestRepository
    {
        public InterestRepository(DbContext context) : base(context)
        {
        }

        public int Count()
        {
            return Context.Set<Interest>().Count();
        }

        public IEnumerable<Interest> GetAll(Guid userId)
        {
            var result = Context.Set<UserInterest>().Where(x => x.UserId == userId);
            var interests = new List<Interest>();
            foreach(var res in result) {
                interests.Add(res.Interest);
            }
            return interests;
        }
    }
}
