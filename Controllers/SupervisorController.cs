using Microsoft.AspNetCore.Mvc;

namespace BlindMatchPAS.Controllers
{
    public class SupervisorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}