using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Data;
using System.Threading.Tasks;

namespace BlindMatchPAS.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // KANISHKA: PLEASE IMPLEMENT THE MATCH REVEAL LOGIC HERE
        [HttpPost]
        public async Task<IActionResult> ConfirmMatch(int proposalId)
        {
            // Empty logic for now
            return RedirectToAction("Index", "Supervisor");
        }
    }
}