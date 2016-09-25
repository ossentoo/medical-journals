using System;
using System.Threading.Tasks;
using MedicalJournals.Entities;
using MedicalJournals.Web.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MedicalJournals.Models.Data;

namespace MedicalJournals.Web.Controllers
{
    public class JournalsController : Controller
    {
        private readonly JournalContext _context;
        private readonly AppSettings _appSettings;

        public JournalsController([FromServices]JournalContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _appSettings = options.Value;
        }
        //
        // GET: /Store/
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();

            return View(categories);
        }

        //
        // GET: /Journals/Browse?category=
        public async Task<IActionResult> Browse(string category)
        {
            // Retrieve category and its associated journals from database
            var categoryModel = await _context.Categories
                .Include(g => g.Journals)
                .Where(g => g.CategoryName == category)
                .FirstOrDefaultAsync();

            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        public async Task<IActionResult> Details(
            [FromServices] IMemoryCache cache,
            Guid id)
        {
            var cacheKey = string.Format("journal_{0}", id);
            Journal journal;
            if (!cache.TryGetValue(cacheKey, out journal))
            {
                journal = await _context.Journals
                                .Where(a => a.JournalId == id)
                                .Include(a => a.Publisher)
                                .Include(a => a.Category)
                                .FirstOrDefaultAsync();

                if (journal != null)
                {
                    if (_appSettings.CacheDbResults)
                    {
                        //Remove it from cache if not retrieved in last 10 minutes
                        cache.Set(
                            cacheKey,
                            journal,
                            new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10)));
                    }
                }
            }

            if (journal == null)
            {
                return NotFound();
            }

            return View(journal);
        }
    }
}