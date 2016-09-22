using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MedicalJournals.Identity
{
    public class JournalRole : IdentityRole<Guid>
    {
        public JournalRole()
        {
            Id = Guid.NewGuid();            
        }
        public JournalRole(string name) 
            :this()
        { Name = name; }
    }
}