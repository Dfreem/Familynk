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
            // get scrapbook and Entries related to the current user.
            Book = await _context.ScrapBooks.Include(s => s.Entries).FirstAsync(sb => sb.FamilyUnitId.Equals(CurrentUser.FamilyUnitId));

            // get saved scraps for the user and save as entries in the scrapbook.
            Book.Entries = _context.Scraps.Include(s => s.Images).Where(sc => sc.ScrapBookId.Equals(Book.ScrapBookId)).ToList();
            ScrapBookVM svm = new()
            {
                Book = Book,
                Family = fam!
            };

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
        public async Task<IActionResult> Create()
        {
            ScrapBookVM svm = new();
            var fam = await _context.Neighborhood
                .Include(n => n.FamilyScraps)
                .ThenInclude(book => book.Entries)
                .Where(s => s.FamilyUnitId
                    .Equals(CurrentUser.FamilyUnitId))
                .FirstOrDefaultAsync(f => f.FamilyUnitId
                    .Equals(CurrentUser.FamilyUnitId));
            svm.Family = fam ?? new();
            return View(svm);
        }

        // POST: Scrapbook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("NewScrap, FileUpload")] ScrapBookVM svm)
        {
            // saving image to local filesystem
            string acceptsFiletypes = "png, jpg, webp";
            foreach (var file in svm.FileUpload)
            {
                // only allow image files, check the file extensions.
                if (svm.FileUpload is null || !acceptsFiletypes.Split(',')
                        .Contains(file.FileName.Split('.')[^1]))
                {
                    return RedirectToAction(nameof(Index));
                }

                var imageFromFile = await FileImageExtract(svm, file);

            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<Image> FileImageExtract(ScrapBookVM svm, IFormFile file)
        {
            var fam = await _context.Neighborhood.Include(f => f.FamilyScraps)
                .FirstAsync(f => f.FamilyUnitId.Equals(CurrentUser.FamilyUnitId));

            // extract file extension, write file to filesystem
            var fileExtension = file.FileName.Split('.')[^1];
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                Image imageFromUpload = new()
                {
                    FileData = memoryStream.ToArray(),
                    FileName = Path.GetRandomFileName().Split('.')[0] + '.' + fileExtension,
                    FileExtension = file.FileName.Split('.')[^1]
                };
                memoryStream.CopyToFile("wwwroot/uploads/" + imageFromUpload.FileName);

                // add image to the image store in the db and add it to the scrap being created.
                svm.NewScrap.Images.Add(imageFromUpload);
                fam.FamilyScraps.Entries.Add(svm.NewScrap);

                // add scrap and parts to the db
                await _context.Images.AddAsync(imageFromUpload);
                await _context.Scraps.AddAsync(svm.NewScrap);
                _context.Neighborhood.Update(fam);

                await _context.SaveChangesAsync();
                return imageFromUpload;
            }

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
