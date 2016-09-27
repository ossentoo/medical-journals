using MedicalJournals.Common.Settings;
using Microsoft.Extensions.Options;

namespace MedicalJournals.Common.Tests
{
    public class TestAppSettings : IOptions<AppSettings>
    {
        public TestAppSettings(bool storeInCache = true)
        {
            Value = new AppSettings()
            {
                SiteTitle = "Medical Journals",
                CacheDbResults = storeInCache
            };
        }

        public AppSettings Value { get; }
    }
}
