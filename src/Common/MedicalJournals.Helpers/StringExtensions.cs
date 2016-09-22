namespace MedicalJournals.Helpers
{
    public static class StringExtensions
    {
        public static string ToNullSafeString(this object value)
        {
            return value == null ? string.Empty : value.ToString();
        }
    }
}