using System;
using System.Data.Entity;
using System.Linq;

using set_basic_aspnet_mvc.Domain.Entities;

namespace set_basic_aspnet_mvc.Domain.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected SetDbContext Context;

        public Repository()
        {
            Context = new SetDbContext();
        }

        public T Create(T entity)
        {
            return Context.Set<T>().Add(entity);
        }

        public T Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void SoftDelete(long id, int deletedBy)
        {
            var entity = Context.Set<T>().Find(id);
            entity.DeletedAt = DateTime.Now;
            entity.DeletedBy = deletedBy;
            entity.IsDeleted = true;
        }

        public void SoftDelete(System.Linq.Expressions.Expression<Func<T, bool>> where, int deletedBy)
        {
            var objects = Context.Set<T>().Where(where).AsEnumerable();
            foreach (var item in objects)
            {
                item.DeletedAt = DateTime.Now;
                item.DeletedBy = deletedBy;
                item.IsDeleted = true;
            }
        }

        public void Delete(long id)
        {
            var entity = Context.Set<T>().Find(id);
            Context.Set<T>().Remove(entity);
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            var objects = Context.Set<T>().Where(where).AsEnumerable();
            foreach (var item in objects)
            {
                Context.Set<T>().Remove(item);
            }
        }

        public T FindOne(System.Linq.Expressions.Expression<Func<T, bool>> where = null, params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(where, includeProperties).FirstOrDefault();
        }

        public IQueryable<T> FindAll(System.Linq.Expressions.Expression<Func<T, bool>> where = null, params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            var items = where != null
                ? Context.Set<T>().Where(s => !s.IsDeleted).Where(where)
                : Context.Set<T>().Where(s => !s.IsDeleted);

            foreach (var property in includeProperties)
            {
                items.Include(property);
            }

            return items;
        }

        public IQueryable AsQueryable()
        {
            return Context.Set<T>();
        }

        public virtual bool SaveChanges()
        {
            return 0 < Context.SaveChanges();
        }
    }
}