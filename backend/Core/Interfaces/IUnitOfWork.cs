using System;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IPostRepository Posts { get; }
        IInterestRepository Interests { get; }
        int Complete();
    }
}
