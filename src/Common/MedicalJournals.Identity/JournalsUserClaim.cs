using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MedicalJournals.Identity
{
    public class JournalUserClaim : IdentityUserClaim<Guid> { }
    public class JournalRoleClaim : IdentityRoleClaim<Guid> { }
}