using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {

        void Add(TEntity entity);

        void Update(TEntity entity);

        bool Delete(TEntity entity);

        IQueryable<TEntity> GetAll();
        

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);


    }
}
