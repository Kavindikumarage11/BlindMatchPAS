using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
    // Start with a query for all proposals
    var proposalsQuery = from p in _context.ProjectProposals
                         select p;

    // Filter by Search String (Title)
    if (!string.IsNullOrEmpty(searchString))
    {
        proposalsQuery = proposalsQuery.Where(s => s.Title!.Contains(searchString));
    }

    // Filter by Category
    if (!string.IsNullOrEmpty(category))
    {
        proposalsQuery = proposalsQuery.Where(x => x.Category == category);
    }

    // Fetch distinct categories for the dropdown menu
    var categories = await _context.ProjectProposals
        .Select(p => p.Category)
        .Distinct()
        .ToListAsync();

    // Store filter data in ViewBag to keep UI states
    ViewBag.Categories = categories;
    ViewBag.CurrentSearch = searchString;
    ViewBag.CurrentCategory = category;

    return View(await proposalsQuery.ToListAsync());
}

        // POST: Supervisor/Approve
        // Handles the project approval/matching logic
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            // 1. Locate the specific proposal by its unique ID
            var proposal = await _context.ProjectProposals.FindAsync(id);

            if (proposal != null)
            {
                // 2. Set the Match status to true
                proposal.IsMatched = true; 

                // 3. Persist changes to the database
                await _context.SaveChangesAsync();
                
                // Optional: Add a success message to TempData
                TempData["Success"] = "Proposal approved successfully!";
            }

            // 4. Redirect back to the index view to refresh the list
            return RedirectToAction(nameof(Index));
        }
    }
}