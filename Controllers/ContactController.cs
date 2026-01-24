using E_project2025.Data;
using E_project2025.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_project2025.Controllers
{
    public class ContactController : Controller
    {
        private readonly E_project2025Context _context;

        public ContactController(E_project2025Context context)
        {
            _context = context;
        }

        // GET: Contact
        [Authorize(Roles = "User")]

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "User")]

        [HttpPost]
        public IActionResult Submit(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                // ✅ Add to DbSet
                _context.ContactModels.Add(model);

                // ✅ Save to DB
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Your message has been sent successfully!";
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }
    }
}
