using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Data;
using BlindMatchPAS.Models;
using Microsoft.EntityFrameworkCore;

namespace BlindMatchPAS.Controllers
{
    public class ProposalController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to initialize the database context
        public ProposalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proposal/Index
        // Retrieves and displays all proposals submitted by the student
        public async Task<IActionResult> Index()
        {
            var proposals = await _context.ProjectProposals.ToListAsync();
            return View(proposals);
        }

        // GET: Proposal/Create
        // Returns the view to create a new project proposal
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proposal/Create
        // Saves the new proposal to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectProposal proposal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proposal);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Proposal submitted successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(proposal);
        }

        // POST: Proposal/RevealSupervisor
        // Logic to flip the identity reveal switch once a match is confirmed
        [HttpPost]
        public async Task<IActionResult> RevealSupervisor(int id)
        {
            var proposal = await _context.ProjectProposals.FindAsync(id);

            if (proposal == null)
            {
                return NotFound();
            }

            // Security Check: Only reveal if the supervisor has already matched it
            if (proposal.IsMatched)
            {
                proposal.IsIdentityRevealed = true;
                await _context.SaveChangesAsync();
                TempData["Message"] = "Supervisor identity has been revealed!";
            }
            else
            {
                TempData["Error"] = "Cannot reveal identity until a match is confirmed.";
            }

            return RedirectToAction(nameof(Index));
        }

    } // End of Class
} // End of Namespace