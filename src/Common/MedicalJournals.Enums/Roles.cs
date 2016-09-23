using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalJournals.Enums
{
    public enum Roles
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Publisher")]
        Publisher,
        [Description("Public")]
        Public,

    }
}
