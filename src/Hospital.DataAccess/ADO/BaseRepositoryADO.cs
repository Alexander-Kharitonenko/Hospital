using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryADO.InterfaceForRepository
{
    public abstract class BaseRepositoryAdo<T> : IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// field contains сonnection string
        /// </summary>
        protected readonly string ConnectionString;

        public BaseRepositoryAdo(string connectionString) 
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// add a new object to the database
        /// </summary>
        /// <param name="entity">object to add</param>
        /// <returns>void</returns>
        public abstract Task CreateEntity(T entity);

        /// <summary>
        /// Delete a object to the database
        /// </summary>
        /// <param name="entity">object to Delete</param>
        /// <returns>void</returns>
        public abstract Task Delete(T entity);

        /// <summary>
        /// get all object from database
        /// </summary>
        /// <returns>void</returns>
        public abstract IEnumerable<T> Get();

        /// <summary>
        /// returns all elements that match a condition
        /// </summary>
        /// <param name="predicate">condition</param>
        /// <returns>IEnumerable<Doctor></returns>
        public abstract IEnumerable<T> GetAllEntityBy(Expression<Func<T, bool>> predicate);

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a object to the database
        /// </summary>
        /// <param name="entity">object to Update</param>
        /// <returns>void</returns>
        public abstract Task Update(T entity);  
    }
}
