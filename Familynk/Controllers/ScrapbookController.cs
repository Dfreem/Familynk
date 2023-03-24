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
            if (svm.FileUpload is null ||
                !acceptsFiletypes.Split(',').Contains(svm.FileUpload.FileName.Split('.')[^1]))
            {
                return RedirectToAction(nameof(Index));
            }

            var imageFromFile = await FileImageExtract(svm, svm.FileUpload);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// protected method specifically for uploading files to Famliynk.
        /// This method contains all the additions needed to insert a <see cref="Familynk.Models.Scrap"></see> into to the database along with an image. The image is converted to a byte array then store in an <see cref="Image"/>.
        /// </summary>
        /// <param name="svm"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        protected async Task<Image> FileImageExtract(ScrapBookVM svm, IFormFile file)
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
                    FileName = file.FileName.Split('.')[0],
                    FileExtension = file.FileName.Split('.')[^1]
                };
                //memoryStream.CopyToFile("wwwroot/uploads/" + imageFromUpload.FileName);

                // add image to the image store in the db and add it to the scrap being created.
                svm.Edit.Images.Add(imageFromUpload);
                fam.FamilyScraps.Entries.Add(svm.Edit);

                // add scrap and parts to the db
                await _context.Images.AddAsync(imageFromUpload);
                await _context.Scraps.AddAsync(svm.Edit);
                _context.Neighborhood.Update(fam);

                await _context.SaveChangesAsync();
                return imageFromUpload;
            }

        }

        public async Task<IActionResult> UpdateScrapImage(int id, [Bind("ScrapBook, FamilyUnit, Scrap ")] ScrapBookVM svm)
        {
            var scrap = await _context.Scraps.FindAsync(svm.Edit.ScrapId);
            var toUpdate = scrap?.Images.Find(i => i.ImageId.Equals(id));
            MemoryStream memoryStream = new();
            svm.FileUpload.CopyTo(memoryStream);
            toUpdate!.FileData = memoryStream.ToArray();
            _context.Scraps.Update(scrap!);
            _context.Images.Update(toUpdate);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit");
        }

        // GET: Scrapbook/Edit/5                                         List
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
            ScrapBookVM svm = new()
            {
                Edit = scrap,
                Family = _context.Neighborhood.Find(CurrentUser.FamilyUnitId)!,
                Book = await _context.ScrapBooks
                    .Include(sb => sb.Entries)
                    .FirstAsync(f => f.FamilyUnitId
                    .Equals(CurrentUser.FamilyUnitId))
            };
            return View(svm);
        }

        // POST: Scrapbook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScrapId,Title,MemberTagId,SenderId")] ScrapBookVM scrap)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(ScrapBookVM), ModelState.GetValidationState(nameof(ScrapBookVM)).Humanize());
                return View(scrap);
            }
            var retrievedScraps = await _context.Scraps.FindAsync(id);
            retrievedScraps?.Comments.AddRange(scrap.Edit.Comments);
            retrievedScraps?.Images.AddRange(scrap.Edit.Images);
            retrievedScraps!.Title = scrap.Edit.Title;
            _context.Scraps.Update(retrievedScraps);
            _context.SaveChanges();
            return View("Index");
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
            var book = await _context.ScrapBooks.FindAsync(scrap!.ScrapBookId);
            if (book != null)
            {
                book.Entries.Remove(scrap!);
            }
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
