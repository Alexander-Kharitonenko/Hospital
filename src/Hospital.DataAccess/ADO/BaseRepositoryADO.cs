using DataAccess.Entity;
using Microsoft.Data.SqlClient;
using Repository.InterfaceForRepositoryADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryADO.InterfaceForRepository
{
    public abstract class BaseRepositoryADO<T> : IRepositoryADO<T> where T : class ,IEntity
    {
        protected readonly string ConnectionString;

        public BaseRepositoryADO(string connectionString) 
        {
            ConnectionString = connectionString;
        }

        public abstract int CreateEntity(T entity);


        public abstract int Delete(T entity);


        public abstract IEnumerable<T> Get();


        public abstract IEnumerable<T> GetAllEntityBy(Expression<Func<T, bool>> predicate);

        public abstract int Update(T entity);
    }
}
