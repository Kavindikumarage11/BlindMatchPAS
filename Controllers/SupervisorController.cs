using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Data;
namespace BlindMatchPAS.Controllers {
    public class SupervisorController : Controller {
        private readonly ApplicationDbContext _context;
        public SupervisorController(ApplicationDbContext context) { _context = context; }
        // Empty - Romain will add Blind Review logic here
    }
}