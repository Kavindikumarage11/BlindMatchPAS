using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlindMatchPAS.Models;
using BlindMatchPAS.Data;

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
        public async Task<IActionResult> Index()
        {
            var proposals = await _context.ProjectProposals.ToListAsync();
            return View(proposals);
        }

        // GET: Proposal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proposal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Abstract,TechStack,Category")] ProjectProposal projectProposal)
        {
            if (ModelState.IsValid)
            {
                projectProposal.IsMatched = false;
                projectProposal.IsIdentityRevealed = false;

                _context.Add(projectProposal);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Proposal submitted successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(projectProposal);
        }
    }
}
