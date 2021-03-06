using System;
using System.Threading;
using System.Threading.Tasks;
using MedicalJournals.Entities;
using MedicalJournals.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using MedicalJournals.Common.Settings;
using MedicalJournals.Web.ViewModels;

namespace MedicalJournals.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize("Admin")]
    public class JournalsManagerController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly JournalContext _context;

        public JournalsManagerController([FromServices]JournalContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _appSettings = options.Value;
        }


        //
        // GET: /JournalsManager/
        public async Task<IActionResult> Index()
        {
            var journals = await _context.Journals
                .Include(a => a.Category)
                .Include(a => a.Publisher)
                .ToListAsync();

            return View(journals);
        }

        //
        // GET: /JournalsManager/Details/5
        public async Task<IActionResult> Details(
            [FromServices] IMemoryCache cache,
            Guid id)
        {
            var cacheKey = GetCacheKey(id);

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
                        //Remove it from cache if not retrieved in last 10 minutes.
                        cache.Set(
                            cacheKey,
                            journal,
                            new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10)));
                    }
                }
            }

            if (journal == null)
            {
                cache.Remove(cacheKey);
                return NotFound();
            }

            return View(journal);
        }

        //
        // GET: /JournalsManager/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "Name");
            ViewBag.PublisherId = new SelectList(_context.Publishers, "PublisherId", "Name");
            return View();
        }

        // POST: /JournalsManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Journal journal,
            [FromServices] IMemoryCache cache,
            CancellationToken requestAborted)
        {
            if (ModelState.IsValid)
            {
                _context.Journals.Add(journal);
                await _context.SaveChangesAsync(requestAborted);

                var journalData = new Journal
                {
                    Title = journal.Title,
                    ImageUrl = Url.Action("Details", "Store", new { id = journal.JournalId })
                };

                cache.Remove("latestJournal");
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "Name", journal.CategoryId);
            ViewBag.PublisherId = new SelectList(_context.Publishers, "PublisherId", "Name", journal.Publisher.PublisherId);
            return View(journal);
        }

        //
        // GET: /JournalsManager/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var journal = await _context.Journals
                .Where(a => a.JournalId== id)
                .Include(p => p.Publisher)
                .Include(p => p.Category)
                .FirstOrDefaultAsync();

            if (journal == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "Name", journal.CategoryId);
            ViewBag.PublisherId = new SelectList(_context.Publishers, "PublisherId", "Name", journal.Publisher.PublisherId);
            return View(Mapper.Map<JournalViewModel>(journal));
        }

        //
        // POST: /JournalsManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            [FromServices] IMemoryCache cache,
            Journal journal,
            CancellationToken requestAborted)
        {
            if (ModelState.IsValid)
            {
                _context.Update(journal);
                await _context.SaveChangesAsync(requestAborted);
                //Invalidate the cache entry as it is modified
                cache.Remove(GetCacheKey(journal.JournalId));
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "Name", journal.CategoryId);
            ViewBag.PublisherId = new SelectList(_context.Publishers, "PublisherId", "Name", journal.Publisher.PublisherId);
            return View(Mapper.Map<JournalViewModel>(journal));
        }

        //
        // GET: /JournalsManager/RemoveJournal/5
        public async Task<IActionResult> RemoveJournal(Guid id)
        {
            var journal = await _context.Journals.Where(a => a.JournalId == id).FirstOrDefaultAsync();
            if (journal == null)
            {
                return NotFound();
            }

            return View(journal);
        }

        //
        // POST: /JournalsManager/RemoveJournal/5
        [HttpPost, ActionName("RemoveJournal")]
        public async Task<IActionResult> RemoveJournalConfirmed(
            [FromServices] IMemoryCache cache,
            Guid id,
            CancellationToken requestAborted)
        {
            var journal = await _context.Journals.Where(a => a.JournalId == id).FirstOrDefaultAsync();
            if (journal == null)
            {
                return NotFound();
            }

            _context.Journals.Remove(journal);
            await _context.SaveChangesAsync(requestAborted);
            //Remove the cache entry as it is removed
            cache.Remove(GetCacheKey(id));

            return RedirectToAction("Index");
        }

        private static string GetCacheKey(Guid id)
        {
            return string.Format("journal_{0}", id);
        }

        // NOTE: this is used for end to end testing only
        //
        // GET: /JournalsManager/GetJournalIdFromName
        // Note: Added for automated testing purpose. Application does not use this.
        [HttpGet]
        [SkipStatusCodePages]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> GetJournalIdFromName(string journalName)
        {
            var journal = await _context.Journals.Where(a => a.Title == journalName).FirstOrDefaultAsync();

            if (journal == null)
            {
                return NotFound();
            }

            return Content(journal.JournalId.ToString());
        }
    }
}