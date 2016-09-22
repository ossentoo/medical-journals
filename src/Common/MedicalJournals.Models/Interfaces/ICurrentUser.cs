using MedicalJournals.Models.Identity;

namespace MedicalJournals.Models.Interfaces
{
    public interface ICurrentUser
    {
        ApplicationUser User { get; }
    }
}