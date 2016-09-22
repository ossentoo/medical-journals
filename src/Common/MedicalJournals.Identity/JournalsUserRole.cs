using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MedicalJournals.Identity
{
    public class JournalUserRole : IdentityUserRole<Guid>
    {
        public JournalUserRole()
        {
            RoleId = Guid.NewGuid();
        }
    }
}