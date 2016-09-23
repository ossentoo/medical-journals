using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MedicalJournals.Helpers;

namespace MedicalJournals.Models.Data
{
    public class Journal
    {
        private long _journalId;

        public long JournalId
        {
            get { return _journalId; }
            set
            {
                _journalId = value;
                QueryId = ShortId.Encode(value);
            }
        }

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public Guid UserId { get; set; }

        public bool? IsEnabled { get; set; }

        public bool? IsPublic { get; set; }

        public DateTime? LastViewed { get; set; }

        public string FileName { get; set; }


        public DateTime? UploadStartTime { get; set; }

        public DateTime? UploadFinishTime { get; set; }

        public int? TimeSpan { get; set; }

        public string OverviewThumbnailPath { get; set; }

        public string QueryId { get; set; }

        public long? ViewCount { get; set; }

        public long? FileSize { get; set; }

        public long? BlockCount { get; set; }

        public bool? IsUploadComplete { get; set; }

        [StringLength(1024)]
        public string UploadStatusMessage { get; set; }

        public short? Width { get; set; }

        public short? Height { get; set; }

        public byte? ResolutionId { get; set; }
        public bool? IsParentalAdvisory { get; set; }

        public virtual Publisher Publisher { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<JournalTag> JournalTags { get; set; }
    }
}