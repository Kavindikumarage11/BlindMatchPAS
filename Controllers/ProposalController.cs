using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Data;
namespace BlindMatchPAS.Controllers {
    public class ProposalController : Controller {
        private readonly ApplicationDbContext _context;
        public ProposalController(ApplicationDbContext context) { _context = context; }
        // Empty - Thejana will add logic here
    }
}