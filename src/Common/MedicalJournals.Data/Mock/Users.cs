using System;
using System.Collections.Generic;
using MedicalJournals.Models.Identity;

namespace MedicalJournals.Data.Mock
{
    public static class Users
    {
        public static ApplicationUser FirstUser { get; private set; }

        public static IEnumerable<ApplicationUser> Get()
        {
            FirstUser = new ApplicationUser
            {
                Id  = Guid.Parse("{98c576f2-2abf-438e-be13-afa60fe0962d}"),
                UserName  = "publisher@publishers.com",
                FirstName = "Test",
                LastName = "User",
                Created = new DateTime(2016, 09, 10)

            };

            FirstUser.Email = FirstUser.UserName;

            var list = new List<ApplicationUser> {
                FirstUser
            };

            return list;
        }
    }
}
