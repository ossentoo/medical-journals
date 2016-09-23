using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MedicalJournals.Models.Identity;
using MedicalJournals.Models.Interfaces;
using MedicalJournals.Helpers;

namespace MedicalJournals.Models.ViewModels
{
    public class UserModel : IHaveCustomMappings
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }

        [Display(Name = "Locked?")]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "Confirmed?")]
        public bool EmailConfirmed { get; set; }


        [Display(Name = "User Type")]
        public string UserType { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<ApplicationUser, UserModel>()
                .ForMember(x => x.UserType, opt => opt.Ignore());
        }
    }
}