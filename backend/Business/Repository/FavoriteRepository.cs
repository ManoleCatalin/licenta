using Core.Domain;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(DbContext context) : base(context)
        {
        }
    }
}
