using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalJournals.Models.ViewModels
{
    public class EditAccountModel
    {
        public IList<SelectListItem> AllRoles { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
