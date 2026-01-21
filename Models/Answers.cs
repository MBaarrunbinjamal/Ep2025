using E_project2025.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_project2025.Models
{
    public class Answers
    {
        public int Id { get; set; }
        public string Answertoquestion { get; set; }
        
        public int SurveyId { get; set; }
        [ForeignKey("SurveyId")]
        public virtual Survay survey { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]

        public virtual E_project2025User users{ get; set; }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]

        public virtual Question questions { get; set; }
    }
}
