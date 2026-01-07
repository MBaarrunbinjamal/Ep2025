using System.ComponentModel.DataAnnotations;

namespace E_project2025.Models
{
    public class Survay
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime  UploadedOn { get; set; }
        public DateTime ExpiryDate { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
