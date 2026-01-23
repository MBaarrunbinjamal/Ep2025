using System.ComponentModel.DataAnnotations;

namespace E_project2025.Models
{
    public class ContactModel
    {
        [Key] // ✅ Primary Key
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
