using System.Collections.Generic;
using MedicalJournals.Models.Data;

namespace MedicalJournals.Data.Mock
{
    public static class Applications 
    {
        public static IEnumerable<Application> Get()
        {
            var author = new Application
            {
                ApplicationId = "1",
                DisplayName = "Medical Journals"
            };

            var list = new List<Application> {
                author
            };

            return list;
        }
    }

}