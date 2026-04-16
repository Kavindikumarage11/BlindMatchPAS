using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlindMatchPAS.Models;
using BlindMatchPAS.Data;
using System.Threading.Tasks;
using System.Linq; // Added for debugging errors

namespace BlindMatchPAS.Controllers
{
    public class ProposalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProposalController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var proposals = await _context.ProjectProposals.ToListAsync();
            return View(proposals);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Abstract,TechStack,Category")] ProjectProposal projectProposal)
        {
            // Set defaults manually to ensure they aren't marked as invalid
            projectProposal.IsMatched = false;
            projectProposal.IsIdentityRevealed = false;

            if (ModelState.IsValid)
            {
                _context.Add(projectProposal);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Proposal submitted successfully!";
                return RedirectToAction(nameof(Index));
            }

            // --- DEBUGGING BLOCK ---
            // If submission fails, this will print the reason in your VS Code Terminal
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    System.Diagnostics.Debug.WriteLine("Form Error: " + error.ErrorMessage);
                }
            }
            // -----------------------

            return View(projectProposal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RevealSupervisor(int id)
        {
            var proposal = await _context.ProjectProposals.FindAsync(id);
            
            if (proposal == null)
            {
                return NotFound();
            }

            if (proposal.IsMatched)
            {
                proposal.IsIdentityRevealed = true;
                _context.Update(proposal);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Supervisor identity revealed successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}