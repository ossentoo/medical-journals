using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MedicalJournals.Data.Interfaces;
using MedicalJournals.Helpers;
using Microsoft.EntityFrameworkCore;

namespace MedicalJournals.Data.Repositories
{

    public class Repository<T> : IRepository<T> where T : class
    {

// ReSharper disable once StaticFieldInGenericType

        private readonly DbContext _context;
        private DbSet<T> DbSet { get; set; }

        public Repository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            _context = dbContext;
            DbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);

            Save();
        }

        public virtual long Count()
        {
            return DbSet.LongCount();
        }

        public virtual long Count(Func<T, bool> @where)
        {
            return DbSet.LongCount(@where);
        }

        public void Delete(T entity)
        {
            var dbEntityEntry = _context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; //Not found, assume already deleted.
            Delete(entity);
        }

        public virtual void Delete(long id, int id2)
        {
            var entity = GetById(id, id2);
            if (entity == null) return; //Not found, assume already deleted.
            Delete(entity);
        }


        public IQueryable<T> Get()
        {
            return DbSet;
        }

        public virtual IList<T> GetAllPaged(Expression<Func<T, DateTime>> expression, int itemsPerPage, int page = 1)
        {
            var dbQuery = _context.Set<T>();
            int toRow = (itemsPerPage * page);
            int fromRow = (toRow - itemsPerPage);

            IList<T> list = dbQuery
                .OrderByDescending(expression)                        
                .AsNoTracking()
                .Skip(fromRow)
                .Take(itemsPerPage)
                .ToList();
            
            return list;
        }
        public virtual IList<T> GetAllPaged(Func<T, bool> @where, Expression<Func<T, DateTime>> expression, int itemsPerPage, int page = 1)
        {
            var dbQuery = _context.Set<T>();
            int toRow = (itemsPerPage * page);
            int fromRow = (toRow - itemsPerPage);

            IList<T> list = dbQuery
                .AsNoTracking()
                .OrderByDescending(expression)
                .Where(@where)
                .Skip(fromRow)
                .Take(itemsPerPage)
                .ToList();
            

            return list;
        }

        public virtual IList<T> GetAllWithIncludes(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            var items = navigationProperties
                        .Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));

            IList<T> list = items.AsNoTracking()
                .ToList();
            
            return list;
        }

        public virtual IEnumerable<T> GetList(Func<T, bool> @where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            var items = navigationProperties
                .Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));

            var list = items.AsNoTracking()
                .Where(@where);
            
            return list;
            
        }

        public virtual IQueryable<T> GetList(Func<T, bool> @where, Func<T, object> orderBy, int take, params Expression<Func<T, object>>[] navigationProperties)
        {

            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            var items = navigationProperties
                .Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));

            return items
                .Where(@where)
                .OrderByDescending(@orderBy)
                .Take(take)
                .AsQueryable();

        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual T GetById(long id)
        {
            return DbSet.Find(id);
        }

        public virtual T GetById(long id, int id2)
        {
            return DbSet.Find(id, id2);
        }

        public virtual T GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual T GetSingle(Func<T, bool> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            var items = navigationProperties
                .Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));

            var item = items.AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(@where);
            
            return item;
        }


        public void Update(T entity)
        {
            DbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            Save();
        }

        private void Save()
        {
            _context.SaveChanges();
        }
    }
}
