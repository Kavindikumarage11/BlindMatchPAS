using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Data;
using Microsoft.EntityFrameworkCore;

namespace BlindMatchPAS.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupervisorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Supervisor Dashboard - List all proposals for review
        public async Task<IActionResult> Index()
        {
            // Database eken okkoma proposals tika gannawa
            var proposals = await _context.ProjectProposals.ToListAsync();
            return View(proposals);
        }

        // Proposal ekak approve karana logic eka
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var proposal = await _context.ProjectProposals.FindAsync(id);
            if (proposal != null)
            {
                proposal.IsMatched = true; // Match una kiyala mark karanawa
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}