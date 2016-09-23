using System;
using System.Collections.Generic;
using MedicalJournals.Models;
using MedicalJournals.Models.Data;

namespace MedicalJournals.Data.Mock
{
    public static class Publishers
    {
        public static IEnumerable<Publisher> Get()
        {
            var author = new Publisher
            {
                User = Users.FirstUser,
                PublisherId = Guid.Parse("{3ca7a9eb-ec08-4bdf-a850-4657d84e48d9}"),
                Name = $"{Users.FirstUser.FirstName} {Users.FirstUser.LastName}",
                Created = new DateTime(2016, 09, 10),
                LastModified = new DateTime(2016, 09, 10)
            };

            var list = new List<Publisher> {
                author
            };

            return list;
        }

    }
}