using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MedicalJournals.Identity
{
    public class JournalUserLogin : IdentityUserLogin<Guid> { }
}