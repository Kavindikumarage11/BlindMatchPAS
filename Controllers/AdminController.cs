using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlindMatchPAS.Data;
using BlindMatchPAS.Models;

namespace BlindMatchPAS.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Dependency Injection: Injecting the Database Context
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Index
        // Displays a comprehensive list of all project proposals for administrative review
        public async Task<IActionResult> Index()
        {
            // Fetching all proposals from the database asynchronously
            var allProposals = await _context.ProjectProposals.ToListAsync();
            
            // Returning the list of proposals to the Admin Index View
            return View(allProposals);
        }
    }
}