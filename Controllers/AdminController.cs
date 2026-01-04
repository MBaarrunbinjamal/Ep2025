using E_project2025.Areas.Identity.Data;
using E_project2025.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_project2025.Controllers
{

    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        E_project2025Context dbcontext;
        private readonly UserAnalyticsService _analytics;
        private readonly UserManager<E_project2025User> _userManager;

        public AdminController(E_project2025Context _dbcontext, UserAnalyticsService analytics, UserManager<E_project2025User> userManager = null)
        {
            dbcontext = _dbcontext;
            _analytics = analytics;
            this._userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.TotalUsers = await _analytics.TotalUsers();
            ViewBag.NewUsersToday = await _analytics.NewUsersToday();
            ViewBag.ActiveUsers = await _analytics.ActiveUsersLast7Days();
            ViewBag.LockedUsers = await _analytics.LockedUsers();

            return View();
        }
        public IActionResult Users() {
            var users = dbcontext.Users.ToList();
            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeUserRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest();

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, role);

            return Json(new { message = "User role updated successfully!" });
        }
        //[ValidateAntiForgeryToken]

        [HttpPost]
        public async Task<IActionResult> Approve(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);

            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            if (!user.isApproved)
            {
                user.isApproved = true;
                await _userManager.UpdateAsync(user); // ✅ correct way
            }

            return Json(new { message = "User has been approved successfully!" });
        }

    }
}
