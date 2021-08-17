using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.InterfaceForRepository
{
    public interface IRepository<T> where T : class, IEntity
    {
        public IQueryable<T> GetAllEntityBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> Get();
        public Task Update(T entity);
        public Task Delete(T entity);
        public Task CreateEntity(T entity);
    }
}
