using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Familynk.Data;
using Familynk.Models;
using NuGet.Packaging;

namespace Familynk.Controllers
{
    [Authorize]
    public class ScrapbookController : Controller
    {
        private readonly SignInManager<FamilyMember> _signInManager;
        private readonly UserManager<FamilyMember> _userManager;
        private readonly INotyfService _toast;
        private readonly FamilyContext _context;
        public FamilyMember CurrentUser { get; set; }
        public ScrapBook? Book { get; set; }

        public ScrapbookController(IServiceProvider services, FamilyContext context)
        {
            _signInManager = services.GetRequiredService<SignInManager<FamilyMember>>();
            _userManager = _signInManager.UserManager;
            _context = context;
            _toast = services.GetRequiredService<INotyfService>();

            string un = _signInManager.Context.User.Identity!.Name!;
            CurrentUser = _userManager.FindByNameAsync(un).Result!;
            var fam = _context.Neighborhood.Find(CurrentUser.FamilyUnitId);
            Book = _context.ScrapBooks.Find(fam!.FamilyScraps.ScrapBookId);
        }

        // GET: Scrapbook
        public async Task<IActionResult> Index()
        {
            var fam = await _context.Neighborhood.FindAsync(CurrentUser.FamilyUnitId);
            if (Book is null)  // TODO change this
            {
                Book = new()
                {
                    Entries = _context.Scraps
                        .ToList()
                        .FindAll((Scrap obj) =>
                            obj.ScrapBookId
                            .Equals(fam!.FamilyScraps.ScrapBookId))
                };
            }

            ScrapBookVM svm = new() { Book = Book, Family = await _context.Neighborhood.FirstAsync(f => f.FamilyUnitId.Equals(CurrentUser.FamilyUnitId)) };
            return View(svm);
        }

        // GET: Scrapbook/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Scraps == null)
            {
                return NotFound();
            }

            var scrap = await _context.Scraps
                .FirstOrDefaultAsync(m => m.ScrapId == id);
            if (scrap == null)
            {
                return NotFound();
            }

            return View(scrap);
        }

        // GET: Scrapbook/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Scrapbook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Book, FileUpload")] ScrapBookVM svm)
        {

            if (svm.FileUpload is not null)
            {
                svm.FileUpload.OpenReadStream().CopyToFile("./uploads/" + svm.FileUpload.FileName);
                string[] getExtention = svm.FileUpload.FileName.Split('.');
                Image image = new()
                {
                    FileName = svm.FileUpload.FileName,
                    FileExtension = getExtention[^1]
                };
                await _context.Images.AddAsync(image);
                var fam = await _context.Neighborhood.FindAsync(CurrentUser.FamilyUnitId);
                var book = await _context.ScrapBooks.FindAsync(fam!.FamilyScraps.ScrapBookId)??new ScrapBook();
                book.Entries = _context.Scraps.Where((Scrap arg1, int arg2) => arg1.ScrapBookId.Equals(fam.FamilyScraps.ScrapBookId)).ToList();
                book!.Entries!.Add(svm.NewScrap);
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Scrapbook/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Scraps == null)
            {
                return NotFound();
            }

            var scrap = await _context.Scraps.FindAsync(id);
            if (scrap == null)
            {
                return NotFound();
            }
            return View(scrap);
        }

        // POST: Scrapbook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScrapId,Title,MemberTagId,SenderId")] Scrap scrap)
        {
            if (id != scrap.ScrapId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scrap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScrapExists(scrap.ScrapId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(scrap);
        }

        // GET: Scrapbook/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Scraps == null)
            {
                return NotFound();
            }

            var scrap = await _context.Scraps
                .FirstOrDefaultAsync(m => m.ScrapId == id);
            if (scrap == null)
            {
                return NotFound();
            }

            return View(scrap);
        }

        // POST: Scrapbook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Scraps == null)
            {
                return Problem("Entity set 'FamilyContext.Scraps'  is null.");
            }
            var scrap = await _context.Scraps.FindAsync(id);
            if (scrap != null)
            {
                _context.Scraps.Remove(scrap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScrapExists(int id)
        {
            return (_context.Scraps?.Any(e => e.ScrapId == id)).GetValueOrDefault();
        }
    }
}
