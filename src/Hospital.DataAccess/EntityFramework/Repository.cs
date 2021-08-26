
using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.EntityFramework
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// context for working with the database
        /// </summary>
        protected readonly HospitalContext ContextDb;

        /// <summary>
        /// the current table with which the repository is running
        /// </summary>
        protected readonly DbSet<T> Table;

        /// <summary>
        /// constructor for initializing fields
        /// </summary>
        /// <param name="contextDb">object for working with a table</param>
        public Repository(HospitalContext contextDb) 
        {
            ContextDb = contextDb;
            Table = contextDb.Set<T>();
        }

        /// <summary>
        /// method for create new entity
        /// </summary>
        /// <param name="Entity">object for working with a table</param>
        /// <returns></returns>
        public async Task CreateEntity(T Entity)
        {
            await Table.AddAsync(Entity);
        }

        /// <summary>
        /// method for delete entity
        /// </summary>
        /// <param name="Entity">object for working with a table</param>
        /// <returns></returns>
        public async Task Delete(T Entity)
        {
            T result = await Table.FirstOrDefaultAsync(el => el.Id == Entity.Id);
            Table.Remove(result);
        }

        /// <summary>
        /// method for get all entity
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Get()
        {
            return Table.AsNoTracking().ToList();
        }

        /// <summary>
        /// method for  get all entity by id
        /// </summary>
        /// <param name="predicate">object for working with a table</param>
        /// <returns></returns>
        public IEnumerable<T> GetAllEntityBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> GetAllEntity = Table.Where(predicate).ToList();
            return GetAllEntity;
            
        }

        /// <summary>
        /// method for save changes 
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChanges()
        {
           var result = await ContextDb.SaveChangesAsync();
           return result;
        }

        /// <summary>
        /// method for update entity
        /// </summary>
        /// <param name="entity">object for working with a table</param>
        /// <returns>void</returns>
        public async Task Update(T entity)
        {         
            Table.Update(entity);
        }
    }
}
