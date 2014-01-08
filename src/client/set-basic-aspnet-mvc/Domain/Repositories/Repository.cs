using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using set_basic_aspnet_mvc.Domain.Entities;

namespace set_basic_aspnet_mvc.Domain.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> 
           where TEntity : BaseEntity
    {
        public TEntity Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(long id, int deletedBy)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(System.Linq.Expressions.Expression<Func<TEntity, bool>> where, int deletedBy)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public void Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> where)
        {
            throw new NotImplementedException();
        }

        public TEntity FindOne(System.Linq.Expressions.Expression<Func<TEntity, bool>> where = null, params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> FindAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> where = null, params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}