using MedicalJournals.Data.Interfaces;
using MedicalJournals.Models;
using MedicalJournals.Models.Data;
using MedicalJournals.Models.Identity;

namespace MedicalJournals.Entities.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Application> Applications { get;  }
        IRepository<Publisher> Publishers { get; }
        IRepository<Category> Categories { get; }
        IRepository<Country> Countries { get; }
        IRepository<Journal> Journals { get; }
        IRepository<ApplicationUser> Users { get; }
        IRepository<Subscription> Subscriptions { get; }

        void Commit();
    }
}
