using E_project2025.Areas.Identity.Data;
using E_project2025.Data;
using E_project2025.DTO;
using E_project2025.DTO;
using E_project2025.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace E_project2025.Controllers
{

   
    public class AdminController : Controller
    {
        E_project2025Context dbcontext;
        private readonly UserAnalyticsService _analytics;
        private readonly UserManager<E_project2025User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(E_project2025Context _dbcontext, UserAnalyticsService analytics, UserManager<E_project2025User> userManager = null, RoleManager<IdentityRole> roleManager = null)
        {
            dbcontext = _dbcontext;
            _analytics = analytics;
            this._userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalUsers = await _analytics.TotalUsers();
            ViewBag.NewUsersToday = await _analytics.NewUsersToday();
            ViewBag.ActiveUsers = await _analytics.ActiveUsersLast7Days();
            ViewBag.LockedUsers = await _analytics.LockedUsers();

            return View();
        }
        //[Authorize(Roles = "Admin")]

        public IActionResult Users() {
            var users = dbcontext.Users.ToList();
            return View(users);
        }
        //[Authorize(Roles = "Admin")]

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
        //[Authorize(Roles = "Admin")]

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
        [Authorize(Roles = "Admin")]

        public IActionResult uploadsurways() {
            return View();
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult createsurvay(string title, DateTime uploadedon, DateTime expiry)
        {
            if(title == null || uploadedon == null || expiry == null)
            {
                return Json(new { error = "All fields should be filled" }); 
            }
            else
            {
                var obj = new Survay()
                {
                    Title=title,
                    UploadedOn = uploadedon,
                    ExpiryDate = expiry,

                };
                dbcontext.Survays.Add(obj);
                dbcontext.SaveChanges();
                return Json(new { success = "survay uploaded successfully"});
            }
         
          
        }
        [Authorize(Roles = "Admin")]

        public IActionResult fetchsurvays ()
        { 
            var a = dbcontext.Survays.ToList();
            return View(a); 
        }

        [Authorize(Roles = "Admin")]

        [Authorize]
    [HttpPost]
    public IActionResult CreateBulk([FromBody] QuestionBulkDto model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
         
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var questionList = model.Questions.Select(q => new Question
        {
            QuestionText = q,
            SurvayId = model.SurvayId,
            UploadedById = userId
        }).ToList();

        dbcontext.Questions.AddRange(questionList);
        dbcontext.SaveChanges();

        return Json(new { success = true, message = "Questions inserted successfully!" });
    }
        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        public async Task<IActionResult> addseminar()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var employees = await _userManager.GetUsersInRoleAsync("Employee");

            var users = admins
                .Union(employees)
                .Distinct()
                .ToList();

            var vm = new SeminarViewModel
            {
                Seminar = new seminar(),
                Users = users
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddSeminar(SeminarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            } 

            dbcontext.seminar.Add(model.Seminar);
            await dbcontext.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        public IActionResult MyAnswers()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var answers = dbcontext.Answers .Include(a => a.questions)
                .Where(a => a.UserId == userId)
                .ToList();

            return View("FetchAnswers", answers);
        }


    }
}
