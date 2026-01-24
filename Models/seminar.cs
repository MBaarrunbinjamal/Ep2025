using E_project2025.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_project2025.Models
{
    public class seminar
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime conductedon { get; set; }
        public string Venue { get; set; }
        public string UserId { get; set; } // Add this property to fix CS0117
    }
}
