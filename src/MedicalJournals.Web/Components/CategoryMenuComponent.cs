using System.Threading.Tasks;
using MedicalJournals.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MedicalJournals.Web.Components
{
    [ViewComponent(Name = "CategoryMenu")]
    public class CategoryMenuComponent : ViewComponent
    {
        public CategoryMenuComponent(JournalContext context)
        {
            DbContext = context;
        }

        private JournalContext DbContext { get; }

        public async Task<IViewComponentResult> InvokeAsync()
        {            
            var categories = await DbContext.Categories.Select(g => g.CategoryName).Take(9).ToListAsync();

            return View(categories);
        }
    }
}