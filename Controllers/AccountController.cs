using Microsoft.AspNetCore.Mvc;

namespace BlindMatchPAS.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}