using Microsoft.AspNetCore.Mvc;

namespace BlindMatchPAS.Controllers
{
    public class AccountController : Controller
    {
        // Display the Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Process the Login Form Submission
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Simple validation: Ensure email and password are not empty
            // In a real scenario, Kanishka will update this to check the Database
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                // If login is successful, redirect to the Home Page
                return RedirectToAction("Index", "Home");
            }

            // If login fails, stay on the login page with an error
            ViewBag.ErrorMessage = "Invalid Login Credentials";
            return View();
        }
    }
}