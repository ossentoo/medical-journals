using System.Collections.Generic;
using System.Linq;
using MedicalJournals.Models;

namespace MedicalJournals.Data.Mock
{
    public static class Journals
    {
        public static IEnumerable<Journal> Get()
        {
            var list = new List<Journal> { 
                new Journal
                {
                    CategoryId  = 1,
                    Title  = "Introduction to Octopus Deploy",
                    Description  = "Octopus Deploy is a first-class tool to implement continuous delivery. This journal will teach you how to configure and use Octopus Deploy to deploy .NET applications.",
                    Publisher = Publishers.Get().FirstOrDefault(),
                }
            };

            return list;
        }
    }
}
