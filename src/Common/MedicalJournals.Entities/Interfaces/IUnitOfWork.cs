using MedicalJournals.Data.Interfaces;
using MedicalJournals.Models;
using MedicalJournals.Models.Data;
using MedicalJournals.Models.Identity;

namespace MedicalJournals.Entities.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Application> Applications { get;  }
        IRepository<Publisher> Authors { get; }
        IRepository<Category> Categories { get; }
        IRepository<Journal> Journals { get; }
        IRepository<ApplicationUser> Users { get; }

        void Commit();
    }
}
