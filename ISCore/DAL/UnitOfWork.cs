
using ISCore.Repositories;
using ISCore.Repositories.Contracts;
using ISCore.UnitofWorks.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ISCore.DAL
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {

        // private readonly SiteDBContext _context;
        private readonly TContext _context;
        private Hashtable _Repositorie;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }
        //public UnitOfWork(SiteDBContext context)
        //      {
        //          _context = context;
        //      }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<UnitOfWorkResult> Saveasync()
        {
            UnitOfWorkResult r = new UnitOfWorkResult();
            try
            {
                await _context.SaveChangesAsync();
                r.IsCompleted = true;
            }
            catch (Exception ex)
            {
                r.Message = ex.Message;
                r.IsCompleted = false;
            }
            return r;
        }


        public UnitOfWorkResult Save()
        {
            UnitOfWorkResult r = new UnitOfWorkResult();
            try
            {
                _context.SaveChanges();
                r.IsCompleted = true;
            }
            catch (Exception ex)
            {
                r.Message = ex.Message;
                r.IsCompleted = false;
            }
            return r;
        }

        public IRepository<T> repo<T>() where T : class
        {
            if (_Repositorie == null)
                _Repositorie = new Hashtable();
            var EntityName = typeof(T).Name;

            if (!_Repositorie.ContainsKey(EntityName))
            {
                var RepositoriesType = typeof(GenericRepository<>);
                var RepositoriesInstance = Activator.CreateInstance(RepositoriesType.MakeGenericType(typeof(T)), _context);
                _Repositorie.Add(EntityName, RepositoriesInstance);
            }
            return (GenericRepository<T>)_Repositorie[EntityName];
        }



    }
}
