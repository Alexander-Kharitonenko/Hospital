using Hospital.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.Interfaces
{
    /// <summary>
    /// interface for implementation Repository
    /// </summary>
    /// <typeparam name="T">the parameter indicates which entity the repository is working with</typeparam>
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// method gets all entities equal to a condition
        /// </summary>
        /// <param name="predicate">indicates conditions</param>
        /// <returns>IEnumerable<T></returns>
        public IEnumerable<T> GetAllEntityBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// method gets all entities 
        /// </summary>
        /// <returns>IEnumerable<T></returns>
        public IEnumerable<T> Get();

        /// <summary>
        /// method for update entities
        /// </summary>
        /// <param name="entity">the essence you need update</param>
        /// <returns>void</returns>
        public Task Update(T entity);

        /// <summary>
        /// method for delete entities
        /// </summary>
        /// <param name="entity">the essence you need delete</param>
        /// <returns></returns>
        public Task Delete(T entity);

        /// <summary>
        /// method for create entity
        /// </summary>
        /// <param name="entity">the essence you need create</param>
        /// <returns>void</returns>
        public Task CreateEntity(T entity);

        /// <summary>
        /// method for save changes
        /// </summary>
        /// <returns>void</returns>
        public Task<int> SaveChanges();
    }
}
