
using DALCore.Repositories.Contracts;

namespace DALCore.UnitofWorks.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        //good Idea
        IRepository<T> repo<T>() where T : class;
        UnitOfWorkResult Save();
        Task<UnitOfWorkResult> Saveasync();

    }
}
