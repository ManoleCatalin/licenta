using Core.Domain;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
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
    }
}
