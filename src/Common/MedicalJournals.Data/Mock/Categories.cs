using System.Collections.Generic;
using System.Linq;
using MedicalJournals.Models.Data;

namespace MedicalJournals.Data.Mock
{
    public static class Categories
    {
        public static IEnumerable<Category> Get()
        {
            var category = new Category
            {
                CategoryId = 1,
                CategoryName = "Default",
                Journals = Journals.Get().ToArray()
            };

            var list = new List<Category> {
                category
            };

            return list;
        }
    }
}