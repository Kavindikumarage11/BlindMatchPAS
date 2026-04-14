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
        // Displays the supervisor dashboard with all submitted project proposals
        public async Task<IActionResult> Index()
        {
            // Fetching all project proposals from the database asynchronously
            var proposals = await _context.ProjectProposals.ToListAsync();
            return View(proposals);
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