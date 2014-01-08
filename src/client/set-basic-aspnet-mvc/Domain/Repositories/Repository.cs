using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using set_basic_aspnet_mvc.Domain.Entities;

namespace set_basic_aspnet_mvc.Domain.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public T Create(T entity)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(long id, int deletedBy)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(System.Linq.Expressions.Expression<Func<T, bool>> where, int deletedBy)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public T FindOne(System.Linq.Expressions.Expression<Func<T, bool>> where = null, params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindAll(System.Linq.Expressions.Expression<Func<T, bool>> where = null, params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable AsQueryable()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}