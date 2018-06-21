using System;
using System.Linq;
using Core.Domain;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        public LikeRepository(DbContext context) : base(context)
        {
        }

        public Guid? GetLikeIdOfPostForUser(Guid postId, Guid userId)
        {
            var like = _entities.FirstOrDefault(l => l.PostId == postId && l.UserId == userId);
            return like?.Id;
        }
    }
}
