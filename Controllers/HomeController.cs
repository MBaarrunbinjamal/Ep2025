using E_project2025.Data;
using E_project2025.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace E_project2025.Controllers
{
    public class HomeController : Controller
    {
        E_project2025Context dbcontext;
        public HomeController(E_project2025Context _dbcontext)
        {
            dbcontext = _dbcontext;
        }
        [Authorize(Roles = "User")]

        public IActionResult Index()
        {
            // Step 1: Get the latest awarded survey
            var latestAwardedSurvey = dbcontext.Survays
                .Where(s => s.status == "Awarded")
                .OrderByDescending(s => s.Id) // or use DateCreated if available
                .FirstOrDefault();

            if (latestAwardedSurvey == null)
            {
                // No awarded survey yet
                return View(null);
            }

            // Step 2: Get the winner (first user who answered this survey)
            var winner = dbcontext.Answers
                .Include(a => a.users)
                .Include(a => a.survey)
                .Where(a => a.SurveyId == latestAwardedSurvey.Id)
                .OrderByDescending(a => a.Id) // latest answer
                .Select(a => new
                {
                    UserName = a.users.UserName,
                    SurveyStatus = a.survey.status,
                    SurveyTitle = a.survey.Title
                })
                .FirstOrDefault();

            return View(winner);
        }

        [Authorize(Roles = "User")]

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize(Roles = "User")]

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize(Roles = "User")]

        public IActionResult FetchSurveys()
        {
            var surveys = dbcontext.Survays
         .Where(s =>
             s.Role == "Everyone" ||
             s.Role == "User" 
         )
         .ToList();


            return View(surveys);
        }
        [Authorize(Roles = "User")]

        public IActionResult FetchQuestions(int surveyId)
        {
            var survey = dbcontext.Survays.Find(surveyId);
            var questions = dbcontext.Questions.Where(q => q.SurvayId == surveyId).ToList();
            ViewBag.surveyName = survey.Title;
            ViewBag.surveyId = survey.Id; 
            return View(questions);
        }
        [Authorize(Roles = "User")]

        [HttpPost]
        public IActionResult SubmitAnswers(int SurveyId, string[] AnswersText, int[] QuestionIds)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            for (int i = 0; i < QuestionIds.Length; i++)
            {
                Answers ans = new Answers
                {
                    SurveyId = SurveyId,
                    QuestionId = QuestionIds[i],
                    Answertoquestion = AnswersText[i],
                    UserId = userId
                };
                dbcontext.Answers.Add(ans);
            }

            dbcontext.SaveChanges();

            return RedirectToAction("FetchSurveys");
        }

        [Authorize(Roles = "User")]

        public IActionResult Seminar()
        {
            var seminars = dbcontext.seminar.ToList();
            return View("seminars", seminars);
        }
        [Authorize(Roles = "User")]

        [HttpPost]
        public IActionResult RegisterSeminar(int seminarId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Json("login-required");

            var alreadyRegistered = dbcontext.SeminarRegistrations
                .Any(x => x.SeminarId == seminarId && x.UserId == userId);

            if (!alreadyRegistered)
            {
                SeminarRegistration reg = new SeminarRegistration
                {
                    SeminarId = seminarId,
                    UserId = userId,
                    RegisteredOn = DateTime.Now
                };

                dbcontext.SeminarRegistrations.Add(reg);
                dbcontext.SaveChanges();
            }

            return Json("success");
        }
        [Authorize(Roles = "User")]

        public void LoadLatestAward()
        {
            if (!User.Identity.IsAuthenticated) return;

            var userId = dbcontext.Users
                .Where(u => u.UserName == User.Identity.Name)
                .Select(u => u.Id)
                .FirstOrDefault();

            var latestAwardedSurvey = dbcontext.Answers
                .Where(a => a.UserId == userId && a.survey.status == "Awarded")
                .OrderByDescending(a => a.survey.AwardedOn)
                .Select(a => a.survey)
                .Distinct()
                .FirstOrDefault();

            ViewBag.LatestAwardedSurvey = latestAwardedSurvey;
        }

       

    }

}
