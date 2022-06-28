using GedaiaPortal.Data;
using GedaiaPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GedaiaPortal.Controllers
{
    [Authorize]
    public class AutomationRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutomationRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AutomationRequest
        public async Task<IActionResult> Index()
        {
            return View(await _context.AutomationRequests.AsNoTracking()
                .Where(x => x.User == User.Identity.Name)
                .ToListAsync());
        }

        // GET: AutomationRequest/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.AutomationRequests == null)
            {
                return NotFound();
            }

            var automationRequest = await _context.AutomationRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (automationRequest == null)
            {
                return NotFound();
            }

            if (automationRequest.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(automationRequest);
        }

        // GET: AutomationRequest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutomationRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description, CreatedAt, LastUpdateDate,User")] AutomationRequest automationRequest)
        {
            if (ModelState.IsValid)
            {
                automationRequest.User = User.Identity.Name;
                _context.Add(automationRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(automationRequest);
        }

        // GET: AutomationRequest/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automationRequest = await _context.AutomationRequests.FindAsync(id);
            if (automationRequest == null)
            {
                return NotFound();
            }
            if (automationRequest.User != User.Identity.Name)
            {
                return NotFound();
            }
            
            return View(automationRequest);
        }

        // POST: AutomationRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Description")] AutomationRequest automationRequest)
        {
            if (id != automationRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    automationRequest.User = User.Identity.Name;
                    automationRequest.LastUpdateDate = DateTime.Now;
                    _context.Update(automationRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutomationRequestExists(automationRequest.Id))
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
            return View(automationRequest);
        }

        // GET: AutomationRequest/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.AutomationRequests == null)
            {
                return NotFound();
            }

            var automationRequest = await _context.AutomationRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (automationRequest == null)
            {
                return NotFound();
            }
            if (automationRequest.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(automationRequest);
        }

        // POST: AutomationRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.AutomationRequests == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AutomationRequests'  is null.");
            }
            var automationRequest = await _context.AutomationRequests.FindAsync(id);
            if (automationRequest != null)
            {
                _context.AutomationRequests.Remove(automationRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutomationRequestExists(long id)
        {
          return (_context.AutomationRequests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
