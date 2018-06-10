using Core.Interfaces;
using System;

namespace Business.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IPostRepository Posts { get; }
        IInterestRepository Interests { get; }
        int Complete();
    }
}
