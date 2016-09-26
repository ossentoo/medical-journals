using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MedicalJournals.Entities;
using MedicalJournals.Models.Data;
using MedicalJournals.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalJournals.Web.ViewModels
{
    public class JournalViewModel : IHaveCustomMappings
    {
        public JournalViewModel([FromServices] JournalContext context)
        {
            Categories = new List<SelectListItem>(context.Categories.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryId.ToString()
            }));
        }

        public string JournalId { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Guid PublisherId { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public DateTime Created { get; set; }

        public List<SelectListItem> Categories { get; set; }
            
        public void CreateMappings(IMapperConfigurationExpression configurationProvider)
        {
            configurationProvider.CreateMap<Journal, JournalViewModel>()
                .ForMember(x => x.Publisher, opt => opt.MapFrom(n=>n.Publisher.Name));
        }
    }
}
