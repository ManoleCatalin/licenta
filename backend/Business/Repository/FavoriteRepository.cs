using System;
using System.Linq;
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

        public Guid? GetFavoriteIdOfPostForUser(Guid postId, Guid userId)
        {
            var favorite = _entities.FirstOrDefault(l => l.PostId == postId && l.UserId == userId);
            return favorite?.Id;   
        }
    }
}
