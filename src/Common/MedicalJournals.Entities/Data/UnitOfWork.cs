using System;
using System.Linq;
using System.Collections.Generic;
using MedicalJournals.Data.Interfaces;
using MedicalJournals.Data.Repositories;
using MedicalJournals.Entities.Interfaces;
using MedicalJournals.Models;
using MedicalJournals.Models.Data;
using MedicalJournals.Models.Identity;

namespace MedicalJournals.Entities.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JournalContext _context;
        private IRepositoryProvider RepositoryProvider { get; set; }
        public IRepository<Application> Applications => GetStandardRepo<Application>();

        public IRepository<Publisher> Authors => GetStandardRepo<Publisher>();
        public IRepository<Country> Countries => GetStandardRepo<Country>();
        public IRepository<Category> Categories => GetStandardRepo<Category>();
        public IRepository<Journal> Journals => GetStandardRepo<Journal>();
        public IRepository<ApplicationUser> Users => GetStandardRepo<ApplicationUser>();

        public UnitOfWork(IRepositoryProvider repositoryProvider, JournalContext context)
        {
            _context = context;
            repositoryProvider.DbContext = _context;
            RepositoryProvider = repositoryProvider;
        }

        protected void CreateDbContext()
        {
            //
            // Allow SecurEntity to add its callbacks
            //

            // Do not enable proxied entities
//            DbContext.Configuration.ProxyCreationEnabled = false;
//            DbContext.Configuration.LazyLoadingEnabled = false;
//            DbContext.Configuration.ValidateOnSaveEnabled = false;
//
//            _dch = new DbContextHelper(DbContext);

        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }
    }

    public class UnitOfWorkMock : IUnitOfWork
    {
        private readonly JournalContext _context;
        private IRepositoryProvider RepositoryProvider { get; set; }
        public IRepository<Application> Applications => GetStandardRepo<Application>();

        public IRepository<Publisher> Authors => GetStandardRepo<Publisher>();
        public IRepository<Country> Countries => GetStandardRepo<Country>();
        public IRepository<Category> Categories => GetStandardRepo<Category>();
        public IRepository<Journal> Journals => GetStandardRepo<Journal>();
        public IRepository<ApplicationUser> Users => GetStandardRepo<ApplicationUser>();


        public void Commit()
        {
            _context.SaveChanges();
        }

        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            var typeParameterType = typeof(T);
            if (typeof(T) is Application)
            {
                return new RepositoryMock<T>((IList<T>) Applications.Get().ToList());
            }

            if (typeParameterType is Publisher)
            {
                return new RepositoryMock<T>((IList<T>)Authors.Get().ToList());
            }

            if (typeParameterType is Category)
            {
                return new RepositoryMock<T>((IList<T>)Categories.Get().ToList());
            }
            if (typeParameterType is Country)
            {
                return new RepositoryMock<T>((IList<T>)Countries.Get().ToList());
            }
            if (typeParameterType is Journal)
            {
                return new RepositoryMock<T>((IList<T>)Journals.Get().ToList());
            }
            if (typeParameterType is ApplicationUser)
            {
                return new RepositoryMock<T>((IList<T>)Users.Get().ToList());
            }

            throw new NotImplementedException();
        }

    }
}
