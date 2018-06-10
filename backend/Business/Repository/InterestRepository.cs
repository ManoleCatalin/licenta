using Core.Domain;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    class InterestRepository : Repository<Interest>, IInterestRepository
    {
        public InterestRepository(DbContext context) : base(context)
        {
        }
    }
}
