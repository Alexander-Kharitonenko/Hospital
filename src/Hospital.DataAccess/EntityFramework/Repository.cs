using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.InterfaceForRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly HospitalContext ContextDb;
        protected readonly DbSet<T> Table;
        public Repository(HospitalContext contextDb) 
        {
            ContextDb = contextDb;
            Table = contextDb.Set<T>();
        }

        public async Task CreateEntity(T Entity)
        {
            await Table.AddAsync(Entity);
        }

        public async Task Delete(T Entity)
        {
            T result = await Table.FirstOrDefaultAsync(el => el.Id == Entity.Id);
            Table.Remove(result);
        }

        public IQueryable<T> Get()
        {
            return Table.AsNoTracking();
        }

        public IQueryable<T> GetAllEntityBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> GetAllEntity = Table.Where(predicate);
            return GetAllEntity;
            
        }

        public async Task Update(T entity)
        {
            T result = await Table.FirstOrDefaultAsync(el => el.Id == entity.Id);
            result = entity;
            Table.Update(result);
        }
    }
}
