using System;
using System.Threading.Tasks;
using MedicalJournals.Entities;
using MedicalJournals.Web.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using AutoMapper;
using MedicalJournals.Models.Data;
using MedicalJournals.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        // GET: /Index/
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();

            return View(categories);
        }

        //
        // GET: /JournalsManager/
        public async Task<IActionResult> Manage()
        {
            var journals = await _context.Journals
                .Include(a => a.Category)
                .Include(a => a.Publisher)
                .ToListAsync();

            return View(journals);
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

        //
        // GET: /JournalsManager/Create
        public IActionResult Create()
        {
            var journal = new JournalViewModel();

            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "Name", journal.CategoryId);
            ViewBag.PublisherId = new SelectList(_context.Publishers, "PublisherId", "Name", journal.PublisherId);

            journal.Categories = _context.Categories.Select(x => new SelectListItem
                                    {
                                        Text = x.CategoryName.ToString(),
                                        Value = x.CategoryId.ToString()
                                    }).ToList();

            if (!string.IsNullOrEmpty(User?.Identity?.Name))
            {
                var publisher = _context.Publishers
                                        .Include(u=>u.User)
                                        .FirstOrDefault(x => x.User.UserName == User.Identity.Name);

                journal.Publisher = publisher.Name;
                journal.PublisherId = publisher.PublisherId;
                journal.UserId = publisher.User.Id;
            }

            // default price for now
            journal.Price = 1;
            return View(journal);
        }

        // POST: /JournalsManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(JournalViewModel journalView)
        {
            if (ModelState.IsValid)
            {
                var journal = Mapper.Map<Journal>(journalView);

                var publisher = _context.Publishers.FirstOrDefault(x => x.PublisherId == journalView.PublisherId);
                journal.Publisher = publisher;
                _context.Journals.Add(journal);
                _context.SaveChanges();

                return RedirectToAction("Manage");
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "Name", journalView.CategoryId);
            ViewBag.PublisherId = new SelectList(_context.Publishers, "PublisherId", "Name", journalView.PublisherId);
            return View(journalView);
        }
    }
}