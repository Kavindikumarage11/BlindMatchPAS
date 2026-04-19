using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Models;
using Microsoft.EntityFrameworkCore;
using BlindMatchPAS.Data;

namespace BlindMatchPAS.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmMatch(int proposalId)
        {
            var proposal = await _context.ProjectProposals.FindAsync(proposalId);

            if (proposal != null)
            {
                proposal.IsMatched = true;
                proposal.IsIdentityRevealed = true;

                _context.Update(proposal);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Match Confirmed!";
            }

            return RedirectToAction("Index", "Supervisor");
        }
    }
}