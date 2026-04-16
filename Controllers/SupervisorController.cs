using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlindMatchPAS.Data;
using BlindMatchPAS.Models;
using System.Threading.Tasks;
using System.Linq;

namespace BlindMatchPAS.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupervisorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Supervisor/Index
        public async Task<IActionResult> Index(string searchString, string category)
        {
            // 1. Start with a query for all proposals
            var proposalsQuery = _context.ProjectProposals.AsQueryable();

            // 2. Filter by Search String (Title)
            if (!string.IsNullOrEmpty(searchString))
            {
                proposalsQuery = proposalsQuery.Where(s => s.Title.Contains(searchString));
            }

            // 3. Filter by Category
            if (!string.IsNullOrEmpty(category))
            {
                proposalsQuery = proposalsQuery.Where(x => x.Category == category);
            }

            // 4. Fetch distinct categories for the dropdown menu
            var categories = await _context.ProjectProposals
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            // Store data in ViewBag for the UI
            ViewBag.Categories = categories;
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentCategory = category;

            return View(await proposalsQuery.ToListAsync());
        }

        // GET: Supervisor/Details/5
        // This makes the "Review Details" button work!
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposal = await _context.ProjectProposals
                .FirstOrDefaultAsync(m => m.Id == id);

            if (proposal == null)
            {
                return NotFound();
            }

            return View(proposal);
        }

        // POST: Supervisor/Match
        // Handles the project matching logic from the Details page or Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Match(int id)
        {
            var proposal = await _context.ProjectProposals.FindAsync(id);

            if (proposal != null)
            {
                proposal.IsMatched = true; 
                _context.Update(proposal);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = "Project matched successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}