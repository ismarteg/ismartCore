using DALCore.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using System.Linq.Expressions;

namespace DALCore.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _db;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _db = _context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _db.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _db.AddRange(entities);
        }

        public void Update(TEntity entity, bool Noattach = true)
        {
            if (!Noattach) _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _db.Update(entity);
        }

        public void Remove(int Id)
        {
            var entity = _db.Find(Id);
            _db.Remove(entity);
        }
        public void Remove(TEntity entity)
        {
            _db.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _db.RemoveRange(entities);
        }


        // Get
        public TEntity Get(int id)
        {
            if (id != 0)
            {
                return _db.Find(id);
            }
            else
            {
                return null;
            }
        }
        public TEntity Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return _db.Find(id);
            }
            else
            {
                return null;
            }
        }
        public TEntity Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                return _db.Find(id);
            }
            else
            {
                return null;
            }
        }


        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _db;

            if (includes != null)
            {
                query = includes(query);
            }
            if (predicate == null)
            {
                return query.AsNoTracking().FirstOrDefault();
            }
            else
            {
                return query.AsNoTracking().FirstOrDefault(predicate);
            }
        }
        public TEntity GetLastOrDefault(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _db;
            if (includes != null)
            {
                query = includes(query);
            }
            if (predicate == null)
            {
                return query.AsNoTracking().LastOrDefault();
            }
            else
            {
                return query.AsNoTracking().LastOrDefault(predicate);
            }
        }
        public TResult GetWithSelect<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _db;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.AsNoTracking().Select(selector).FirstOrDefault();
        }



        // Get All
        public IEnumerable<TEntity> GetAll()
        {
            return _db.AsNoTracking().ToList();
        }
        public IEnumerable<TEntity> GetAll(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {

            IQueryable<TEntity> query = _db;


            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includes != null)
            {
                query = includes(query);

            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.AsNoTracking().ToList();

        }


        public IEnumerable<TResult> GetAll<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _db;

            if (includes != null)
            {
                query = includes(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return query.Select(selector);
        }

        public IEnumerable<TEntity> GetPage(int page, int pagesize, out int count,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _db;

            if (includes != null)
            {
                query = includes(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            count = query.Count();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return query.Skip((page - 1) * pagesize).Take(pagesize);
        }

        public IEnumerable<TResult> GetPage<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate,
            int page, int pagesize, out int count,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _db;

            if (includes != null)
            {
                query = includes(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            count = query.Count();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return query.Skip((page - 1) * pagesize).Take(pagesize).Select(selector);
        }

        public int GetCount()
        {

            return _db.AsNoTracking().Count();

        }

        public int GetCount(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                return _db.Count();
            }
            else
            {
                return _db.Count(predicate);
            }
        }




        /// <summary>
        /// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An <see cref="IQueryable{TEntity}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
        public virtual IQueryable<TEntity> GetFromSql(string sql, params object[] parameters) => _db.FromSqlRaw(sql, parameters);


    }
}
