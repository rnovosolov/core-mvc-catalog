using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreMVCcatalog;
using CoreMVCcatalog.Models;
using CoreMVCcatalog.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoreMVCcatalog.Controllers
{
    public class FoldersController : Controller
    {
        private readonly IFolderService _folderService;
        private readonly ApplicationDbContext _context;

        public FoldersController(ApplicationDbContext context , IFolderService folderService)
        {
            _context = context;
            _folderService = folderService;
        }



        // GET: Folders
        public async Task<IActionResult> Index()
        {
              return _context.Folders != null ? 
                          View(await _context.Folders.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Folders'  is null.");
        }

        // GET: Folders/Details/5
        public async Task<IActionResult> Details(string? name)
        {
            if (name == null || _context.Folders == null)
            {
                return NotFound();
            }

            var folder = await _context.Folders.
                Include(f => f.Subfolders).FirstOrDefaultAsync(f => f.Name == name);

            if (folder == null)
            {
                return NotFound();
            }

            return View(folder);
        }

        // GET: Folders/Create
        public IActionResult Create(int parentId)
        {
            Folder folder = new Folder();
            folder.parent = _context.Folders.Find(parentId);

            return View(folder);
        }

        // POST: Folders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name, parent")] Folder folder)
        {

            if (ModelState.IsValid)
            {
                _folderService.AddFolder(folder);
                return RedirectToAction(nameof(Index));
            }
            return View(folder);
        }

        // GET: Folders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Folders == null)
            {
                return NotFound();
            }

            var folder = await _context.Folders.FindAsync(id);
            if (folder == null)
            {
                return NotFound();
            }
            return View(folder);
        }

        // POST: Folders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Folder folder)
        {
            if (id != folder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(folder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FolderExists(folder.Id))
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
            return View(folder);
        }

        // GET: Folders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Folders == null)
            {
                return NotFound();
            }

            var folder = await _context.Folders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (folder == null)
            {
                return NotFound();
            }

            return View(folder);
        }

        // POST: Folders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Folders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Folders'  is null.");
            }
            var folder = await _context.Folders.FindAsync(id);
            if (folder != null)
            {
                _context.Folders.Remove(folder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FolderExists(int id)
        {
          return (_context.Folders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
