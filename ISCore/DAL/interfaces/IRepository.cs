using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ISCore.DAL.interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(int id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity, bool Noattach = true);



        TEntity Get(int id);
        TEntity Get(string id);
        TEntity Get(Guid id);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

        TEntity GetLastOrDefault(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);


        TResult GetWithSelect<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate);


        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

        IEnumerable<TResult> GetAll<TResult>(
       Expression<Func<TEntity, bool>> predicate,
       Expression<Func<TEntity, TResult>> selector,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);


        IEnumerable<TEntity> GetPage(
        int page, int pagesize, out int count,
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

        IEnumerable<TResult> GetPage<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate,
            int page, int pagesize, out int count,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

        /// <summary>
        /// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An <see cref="IQueryable{TEntity}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
        IQueryable<TEntity> GetFromSql(string sql, params object[] parameters);

        int GetCount();
        int GetCount(Expression<Func<TEntity, bool>> predicate);



    }
}
