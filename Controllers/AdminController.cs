using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Data;
namespace BlindMatchPAS.Controllers {
    public class AdminController : Controller {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context) { _context = context; }
        // Empty - Didula will add Admin Oversight logic here
    }
}