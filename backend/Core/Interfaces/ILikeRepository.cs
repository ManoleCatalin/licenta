using System;
using Core.Domain;

namespace Core.Interfaces
{
    public interface ILikeRepository : IRepository<Like>
    {

        Guid? GetLikeIdOfPostForUser(Guid id, Guid userId);
    }
}
