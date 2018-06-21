using System;
using Core.Domain;

namespace Core.Interfaces
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        Guid? GetFavoriteIdOfPostForUser(Guid id, Guid userId);
    }
}
