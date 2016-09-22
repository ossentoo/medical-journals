using System;
using Microsoft.EntityFrameworkCore;

namespace MedicalJournals.Data.Interfaces
{
    public interface IRepositoryProvider
    {
        DbContext DbContext { get; set; }
        IRepository<T> GetRepositoryForEntityType<T>() where T : class;
        T GetRepository<T>(Func<DbContext, object> factory = null) where T : class;
    }
}
