using DataAccess.Entity;
using Hospital.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryADO.InterfaceForRepository
{
    public abstract class BaseRepositoryADO<T> : IRepository<T> where T : class ,IEntity
    {
        protected readonly string ConnectionString;

        public BaseRepositoryADO(string connectionString) 
        {
            ConnectionString = connectionString;
        }

        public abstract Task CreateEntity(T entity);

        public abstract Task Delete(T entity);

        public abstract IEnumerable<T> Get();

        public abstract IEnumerable<T> GetAllEntityBy(Expression<Func<T, bool>> predicate);

        public abstract Task Update(T entity);

       
    }
}
