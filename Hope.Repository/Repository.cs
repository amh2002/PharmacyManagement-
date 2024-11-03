using Hope.EntityComponent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private DbSet<TEntity> dbSet;
        private readonly HopePharmacyManagementContext context;
        public Repository() {

            this.context = new HopePharmacyManagementContext();
            this.dbSet = context.Set<TEntity>();
            
        }

        public void Add(TEntity entity) { 


            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void Update(TEntity entity) {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Update(entity);
            context.SaveChanges();
        
        }

        public bool Delete(TEntity entity) {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Remove(entity);
            context.SaveChanges();
            return true; 
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var item in includes)
            {

                query = query.Include(item);

            }
            return query.Where(expression);
        }

    }
}
