using E_project2025.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_project2025.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }

        public int SurvayId { get; set; }
        public Survay Survay { get; set; }

        public string UploadedById { get; set; }
        public E_project2025User User { get; set; }
    }
}

