using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using MedicalJournals.Models.Data;
using MedicalJournals.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalJournals.Web.ViewModels
{
    public class JournalViewModel : IHaveCustomMappings
    {
        public JournalViewModel()
        {
            Created = DateTime.UtcNow;
            LastModified = DateTime.UtcNow;
        }

        public DateTime LastModified { get; set; }
        public string JournalId { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Guid PublisherId { get; set; }
        public Guid UserId { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public DateTime Created { get; set; }
        public IFormFile File { get; set; }

        public List<SelectListItem> Categories { get; set; }
            
        public void CreateMappings(IMapperConfigurationExpression configurationProvider)
        {

            configurationProvider.CreateMap<Journal, JournalViewModel>()
                .ForMember(x => x.Publisher, opt => opt.MapFrom(n=>n.Publisher.Name))
                .ForMember(x => x.PublisherId, opt => opt.MapFrom(n => n.Publisher.PublisherId))
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x => x.Categories, opt => opt.MapFrom(n => GetSelectListItems()));

            configurationProvider.CreateMap<JournalViewModel, Journal>()
                .ForMember(x => x.LastViewed, opt => opt.Ignore())
                .ForMember(x => x.IsEnabled, opt => opt.Ignore())
                .ForMember(x => x.IsPublic, opt => opt.Ignore())
                .ForMember(x => x.UploadStartTime, opt => opt.Ignore())
                .ForMember(x => x.UploadFinishTime, opt => opt.Ignore())
                .ForMember(x => x.TimeSpan, opt => opt.Ignore())
                .ForMember(x => x.QueryId, opt => opt.Ignore())
                .ForMember(x => x.ViewCount, opt => opt.Ignore())
                .ForMember(x => x.FileSize, opt => opt.Ignore())
                .ForMember(x => x.BlockCount, opt => opt.Ignore())
                .ForMember(x => x.IsUploadComplete, opt => opt.Ignore())
                .ForMember(x => x.UploadStatusMessage, opt => opt.Ignore())
                .ForMember(x => x.Width, opt => opt.Ignore())
                .ForMember(x => x.Height, opt => opt.Ignore())
                .ForMember(x => x.ResolutionId, opt => opt.Ignore())
                .ForMember(x => x.IsParentalAdvisory, opt => opt.Ignore())
                .ForMember(x => x.Category, opt => opt.Ignore())
                .ForMember(x => x.JournalTags, opt => opt.Ignore())
                .ForMember(x => x.Publisher, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.MapFrom(n => ConvertToBytes(n.File)));

        }

        private byte[] ConvertToBytes(IFormFile file)
        {
            var stream = file.OpenReadStream();
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private List<SelectListItem> GetSelectListItems()
        {
            var selectListItems = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "Alternative and traditional"},
                new SelectListItem {Value = "2", Text = "Anatomy"},
                new SelectListItem {Value = "3", Text = "Anesthesiology and palliative medicine"},
                new SelectListItem {Value = "4", Text = "Behavioral"},
                new SelectListItem {Value = "5", Text = "Cardiology"},
                new SelectListItem {Value = "6", Text = "Dentistry"},
                new SelectListItem {Value = "7", Text = "Dermatology"},
                new SelectListItem {Value = "8", Text = "Epidemiology"},
                new SelectListItem {Value = "9", Text = "Gastroenterology and hepatology"},
                new SelectListItem {Value = "10", Text = "Hematology"},
                new SelectListItem {Value = "11", Text = "Immunology"},
                new SelectListItem {Value = "12", Text = "Laboratory"},
                new SelectListItem {Value = "13", Text = "Microbiology"},
                new SelectListItem {Value = "14", Text = "Neurology"},
                new SelectListItem {Value = "15", Text = "Nutrition"},
                new SelectListItem {Value = "16", Text = "Oncology"},
                new SelectListItem {Value = "17", Text = "Pediatrics"},
                new SelectListItem {Value = "18", Text = "Psychiatry"},
                new SelectListItem {Value = "19", Text = "Rheumatology"},
                new SelectListItem {Value = "20", Text = "Sports medicine"},
                new SelectListItem {Value = "21", Text = "Toxicology"},
                new SelectListItem {Value = "22", Text = "Urology"},
                new SelectListItem {Value = "23", Text = "Veterinary"}
            };

            return selectListItems;
        }
    }
}
