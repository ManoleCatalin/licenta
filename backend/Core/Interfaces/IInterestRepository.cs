using Core.Domain;
using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IInterestRepository : IRepository<Interest>
    {
        int Count();
        IEnumerable<Interest> GetAll(Guid userId);
    }
}
