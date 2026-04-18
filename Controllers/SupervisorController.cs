```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlindMatchPAS.Data;

namespace BlindMatchPAS.Controllers
{
    // OOP Concepts Used:
    // Encapsulation - The _context variable is kept private and accessed only within this class
    // Inheritance - SupervisorController inherits from the Controller base class
    // Abstraction - Uses IActionResult to abstract different types of HTTP responses
    // Polymorphism - Method Index overrides behavior from the base Controller class

    public class SupervisorController : Controller
    {
        // Stores the database context used to interact with the database
        private readonly ApplicationDbContext _context;

        // Constructor to initialize the database context using dependency injection
        public SupervisorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // This method retrieves all unmatched project proposals
        // Optionally filters proposals based on the provided category
        // Returns the filtered list to the view
        public async Task<IActionResult> Index(string searchCategory)
        {
            // Holds the query for project proposals that are not yet matched
            var proposals = from p in _context.ProjectProposals
                           where p.IsMatched == false
                           select p;

            // Checks if a category filter is provided and applies it
            if (!string.IsNullOrEmpty(searchCategory))
            {
                proposals = proposals.Where(s => s.Category == searchCategory);
            }

            // Executes the query and passes the result list to the view
            return View(await proposals.ToListAsync());
        }
    }
}
```
