using System.ComponentModel;

namespace MedicalJournals.Enums
{
    public enum UType : byte
    {
        [Description("Publisher")]
        Publisher = 1,
        [Description("Public")]
        Public,
    }
}