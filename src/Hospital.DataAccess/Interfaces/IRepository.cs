using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.Interface
{
    public interface IRepository<T> where T : class, IEntity
    {
        public IEnumerable<T> GetAllEntityBy(Expression<Func<T, bool>> predicate);
        public IEnumerable<T> Get();
        public Task Update(T entity);
        public Task Delete(T entity);
        public Task CreateEntity(T entity);
    }
}
