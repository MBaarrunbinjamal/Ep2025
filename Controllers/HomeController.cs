using System.Diagnostics;
using E_project2025.Data;
using E_project2025.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_project2025.Controllers
{
    public class HomeController : Controller
    {
        E_project2025Context dbcontext;
        public HomeController(E_project2025Context _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
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
        public IActionResult FetchQuestions(int surveyId)
        {
            var survey = dbcontext.Survays.Find(surveyId);
            var questions = dbcontext.Questions.Where(q => q.SurvayId == surveyId).ToList();
            ViewBag.surveyName = survey.Title;
            ViewBag.surveyId = survey.Id; 
            return View(questions);
        }
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

    }

}
