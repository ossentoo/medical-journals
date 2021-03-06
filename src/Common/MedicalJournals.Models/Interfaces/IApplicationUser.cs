using System;
using MedicalJournals.Enums;

namespace MedicalJournals.Models.Interfaces
{
    public interface IApplicationUser  
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime Created { get; set; }
    }
}