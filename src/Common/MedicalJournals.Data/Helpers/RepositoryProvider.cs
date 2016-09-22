using System;
using System.Collections.Generic;
using MedicalJournals.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicalJournals.Data.Helpers
{
    public class RepositoryProvider : IRepositoryProvider
    {
        private readonly RepositoryFactory _repositoryFactory;

        public RepositoryProvider(RepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            Repositories = new Dictionary<Type, object>();
        }

        public DbContext DbContext { get; set; }

        public IRepository<T> GetRepositoryForEntityType<T>() where T : class
        {
            return GetRepository<IRepository<T>>(
                _repositoryFactory.GetRepositoryFactoryForEntityType<T>());
        }

        public IRepository<T> GetMockRepositoryForEntityType<T>() where T : class
        {
            return GetRepository<IRepository<T>>(
                _repositoryFactory.GetMockRepositoryFactoryForEntityType<T>());
        }

        public virtual T GetRepository<T>(Func<DbContext, object> factory = null) where T : class
        {
            // Look for T dictionary cache under typeof(T)
            object repoObj;
            Repositories.TryGetValue(typeof(T), out repoObj);
            if (repoObj != null)
            {
                return (T)repoObj;
            }

            // Not found or null; make one, add to dictionary
            return MakeRepository<T>(factory, DbContext);
        }

        protected Dictionary<Type, object> Repositories { get; private set; }

        protected virtual T MakeRepository<T>(Func<DbContext, object> factory, DbContext dbContext) where T : class
        {
            var f = factory ?? _repositoryFactory.GetRepositoryFactory<T>();

            if (f == null)
            {
                throw new NotImplementedException("No factory for repository type, " + typeof(T));
            }

            var repo = (T)f(dbContext);
            Repositories[typeof(T)] = repo;
            return repo;
        }

        public void SetRepository<T>(T repository)
        {
            Repositories[typeof(T)] = repository;
        }
    }
}
