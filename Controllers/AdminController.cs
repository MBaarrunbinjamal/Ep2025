using Microsoft.AspNetCore.Mvc;

namespace E_project2025.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
