using System;
using AutoMapper;
using MedicalJournals.Models.Data;
using MedicalJournals.Models.Interfaces;

namespace MedicalJournals.Models.ViewModels
{
    public class JournalViewModel : IHaveCustomMappings
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public DateTime Created { get; set; }
        public void CreateMappings(IMapperConfigurationExpression configurationProvider)
        {
            configurationProvider.CreateMap<Journal, JournalViewModel>()
                .ForMember(x => x.Publisher, opt => opt.MapFrom(n=>n.Publisher.Name));
        }
    }
}
