using E_project2025.Data;
using E_project2025.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_project2025.Controllers
{
    public class FacultyController : Controller
    {
        E_project2025Context dbcontext;
        public FacultyController(E_project2025Context _dbcontext)
        {
            dbcontext = _dbcontext;
        }
        [Authorize(Roles = "Faculty")]

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Faculty")]

        public IActionResult ViewSurveys()
        {
            return View();
        }
        [Authorize(Roles = "Faculty")]

        public IActionResult FetchSurveys()
        {
            var surveys = dbcontext.Survays
         .Where(s =>
             s.Role == "Everyone" ||
             s.Role == "Faculty"
         )
         .ToList();


            return View(surveys);
        }
        [Authorize(Roles = "Faculty")]

        public IActionResult FetchfacultyQuestions(int surveyId)
        {
            var survey = dbcontext.Survays.Find(surveyId);
            var questions = dbcontext.Questions.Where(q => q.SurvayId == surveyId).ToList();
            ViewBag.surveyName = survey.Title;
            ViewBag.surveyId = survey.Id;
            return View(questions);
        }
        [Authorize(Roles = "Faculty")]

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
