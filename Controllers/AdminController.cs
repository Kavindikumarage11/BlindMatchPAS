using Microsoft.AspNetCore.Mvc;

namespace BlindMatchPAS.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}