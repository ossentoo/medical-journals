using System.Collections.Generic;
using System.Linq;
using MedicalJournals.Models.Data;

namespace MedicalJournals.Data.Mock
{
    public static class Countries
    {
        public static IEnumerable<Country> Get()
        {
            var country1 = new Country
            {
                CountryId = 1,
                CountryName = "Uganda",
                CountryCode = "UG"
            };

            var country2 = new Country
            {
                CountryId = 1,
                CountryName = "United Kingdom",
                CountryCode = "UK"
            };

            var country3 = new Country
            {
                CountryId = 1,
                CountryName = "United States",
                CountryCode = "US"
            };

            var list = new List<Country> {
                country1, country2, country3
            };

            return list;
        }
    }
}