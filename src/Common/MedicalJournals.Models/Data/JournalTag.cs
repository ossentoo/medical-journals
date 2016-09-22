namespace MedicalJournals.Models.Data
{
    public class JournalTag
    {
        public long JournalId { get; set; }
        public Journal Journal { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
