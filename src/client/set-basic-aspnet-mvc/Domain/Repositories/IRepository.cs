using System;
using System.Linq;
using System.Linq.Expressions;

using set_basic_aspnet_mvc.Domain.Entities;

namespace set_basic_aspnet_mvc.Domain.Repositories
{
    public interface IRepository<TEntity>
           where TEntity : BaseEntity
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);

        void SoftDelete(long id, int deletedBy);
        void SoftDelete(Expression<Func<TEntity, bool>> where, int deletedBy);

        void Delete(long id);
        void Delete(Expression<Func<TEntity, bool>> where);

        TEntity FindOne(Expression<Func<TEntity, bool>> where = null, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> where = null, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable AsQueryable();

        bool SaveChanges();
    }   
}