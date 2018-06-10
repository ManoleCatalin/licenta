using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;


        public UnitOfWork(DbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Posts = new PostRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public IPostRepository Posts { get; private set; }
        public IInterestRepository Interests { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
