using Core.Domain;

namespace Core.Interfaces
{
    public interface IInterestRepository : IRepository<Interest>
    {
        int Count();
    }
}
