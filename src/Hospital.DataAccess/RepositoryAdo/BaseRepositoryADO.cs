using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.DataAccess.RepositoryAdo
{
    /// <summary>
    /// Base implementation class
    /// </summary>
    /// <typeparam name="T">a parameter responsible for the entity with which the class works</typeparam>
    public abstract class BaseRepositoryAdo<T> : IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// field contains сonnection string
        /// </summary>
        protected readonly string ConnectionString;

        /// <summary>
        /// constructor for initializing class fields
        /// </summary>
        /// <param name="connectionString">database connection field</param>
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
