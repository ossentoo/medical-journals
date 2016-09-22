using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MedicalJournals.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicalJournals.Data.Repositories
{
    public class RepositoryMock<T> : IRepository<T> where T : class
    {

// ReSharper disable once StaticFieldInGenericType

        private readonly IList<T> _context;
        private DbSet<T> DbSet { get; set; }

        public RepositoryMock(IList<T> context)
        {
            if (context == null)
                throw new ArgumentNullException("dbContext");

            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);

            Save();
        }

        public virtual long Count()
        {
            return _context.Count;
        }

        public virtual long Count(Func<T, bool> @where)
        {
            return _context.Count;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);            
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
            return _context.AsQueryable();
        }

        public virtual IList<T> GetAllPaged(Expression<Func<T, DateTime>> expression, int itemsPerPage, int page = 1)
        {
            int toRow = (itemsPerPage * page);
            int fromRow = (toRow - itemsPerPage);

            IList<T> list = _context.AsQueryable()
                .OrderByDescending(expression)                        
                .Skip(fromRow)
                .Take(itemsPerPage)
                .ToList();
            
            return list;
        }
        public virtual IList<T> GetAllPaged(Func<T, bool> @where, Expression<Func<T, DateTime>> expression, int itemsPerPage, int page = 1)
        {
            int toRow = (itemsPerPage * page);
            int fromRow = (toRow - itemsPerPage);

            IList<T> list = _context.AsQueryable()
                .OrderByDescending(expression)
                .Where(@where)
                .Skip(fromRow)
                .Take(itemsPerPage)
                .ToList();
            

            return list;
        }

        public virtual IList<T> GetAllWithIncludes(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.AsQueryable();

            //Apply eager loading
            var items = navigationProperties
                        .Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));

            IList<T> list = items.AsNoTracking()
                .ToList();
            
            return list;
        }

        public virtual IEnumerable<T> GetList(Func<T, bool> @where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.AsQueryable();

            //Apply eager loading
            var items = navigationProperties
                .Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));

            var list = items.AsNoTracking()
                .Where(@where);
            
            return list;
            
        }

        public virtual IQueryable<T> GetList(Func<T, bool> @where, Func<T, object> orderBy, int take, params Expression<Func<T, object>>[] navigationProperties)
        {

            IQueryable<T> dbQuery = _context.AsQueryable();

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
            return _context.FirstOrDefault();
        }

        public virtual T GetById(long id)
        {
            return _context.FirstOrDefault();
        }

        public virtual T GetById(long id, int id2)
        {
            return _context.FirstOrDefault();
        }

        public virtual T GetById(Guid id)
        {
            return _context.FirstOrDefault();
        }

        public virtual T GetSingle(Func<T, bool> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.AsQueryable();

            //Apply eager loading
            var items = navigationProperties
                .Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));

            var item = items.AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(@where);
            
            return item;
        }


        public void Update(T entity)
        {

            Save();
        }

        private void Save()
        {
            
        }
    }
}
