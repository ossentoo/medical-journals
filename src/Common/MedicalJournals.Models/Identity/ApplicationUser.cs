using System;
using MedicalJournals.Models.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MedicalJournals.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
    }
}
