using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlindMatchPAS.Data;
using BlindMatchPAS.Models;

namespace BlindMatchPAS.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupervisorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchCategory)
        {
            var proposals = from p in _context.ProjectProposals
                           where p.IsMatched == false
                           select p;

            if (!string.IsNullOrEmpty(searchCategory))
            {
                proposals = proposals.Where(s => s.Category == searchCategory);
            }

            return View(await proposals.ToListAsync());
        }
    }
}