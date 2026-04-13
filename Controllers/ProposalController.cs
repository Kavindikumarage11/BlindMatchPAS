using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Data;
using BlindMatchPAS.Models;
using Microsoft.EntityFrameworkCore;

namespace BlindMatchPAS.Controllers
{
    public class ProposalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProposalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proposal/Index
        // Displays the list of all submitted proposals
        public async Task<IActionResult> Index()
        {
            var proposals = await _context.ProjectProposals.ToListAsync();
            return View(proposals);
        }

        // GET: Proposal/Create
        // Displays the form to submit a new project proposal
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proposal/Create
        // Handles the submission and saves the proposal to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectProposal proposal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proposal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proposal);
        }

        // POST: Proposal/RevealSupervisor
        // Logic to reveal the supervisor identity once a match is confirmed
        [HttpPost]
        public async Task<IActionResult> RevealSupervisor(int id)
        {
            var proposal = await _context.ProjectProposals.FindAsync(id);

            if (proposal == null)
            {
                return NotFound();
            }

            // Only reveal identity if the supervisor has already matched/approved it
            if (proposal.IsMatched)
            {
                proposal.IsIdentityRevealed = true;
                await _context.SaveChangesAsync();
                TempData["Message"] = "Supervisor identity revealed successfully!";
            }
            else
            {
                TempData["Error"] = "This proposal has not been matched yet.";
            }

            return RedirectToAction(nameof(Index));
        }

    } // End of Class
} // End of Namespace