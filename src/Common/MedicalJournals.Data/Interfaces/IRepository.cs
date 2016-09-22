using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MedicalJournals.Data.Interfaces
{
    public interface IRepository<T>
    {
        IList<T> GetAllPaged(Expression<Func<T, DateTime>> expression, int itemsPerPage, int page = 1);
        IList<T> GetAllPaged(Func<T, bool> @where, Expression<Func<T, DateTime>> expression, int itemsPerPage, int page = 1);
        IList<T> GetAllWithIncludes(params Expression<Func<T, object>>[] navigationProperties);
        IEnumerable<T> GetList(Func<T, bool> @where, params Expression<Func<T, object>>[] navigationProperties);

        IQueryable<T> GetList(Func<T, bool> @where, Func<T, object> orderBy, int take, params Expression<Func<T, object>>[] navigationProperties);
        
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        IQueryable<T> Get();
        T GetById(int id);
        T GetById(long id);
        T GetById(long id, int id2);
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void Delete(long id, int id2);
        long Count();
        long Count(Func<T, bool> @where);
    }
}
