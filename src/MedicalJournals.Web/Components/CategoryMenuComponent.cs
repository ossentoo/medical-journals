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
        private readonly JournalContext _context;

        public CategoryMenuComponent([FromServices] JournalContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {            
            var categories = await _context.Categories.Select(g => g.CategoryName).Take(9).ToListAsync();

            return View(categories);
        }
    }
}