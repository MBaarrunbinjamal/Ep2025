using System.ComponentModel.DataAnnotations;

namespace E_project2025.Models
{
    public class Survay
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public DateTime AwardedOn { get; set; }

        public string Role { get; set; }
        public string status { get; set; } = "on going";
        public DateTime  UploadedOn { get; set; }=DateTime.Now;
        public DateTime ExpiryDate { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
