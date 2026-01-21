using E_project2025.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_project2025.Models
{
    public class seminar
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string venue { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual E_project2025User users { get; set; }
    }
}
