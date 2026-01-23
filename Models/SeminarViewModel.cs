using E_project2025.Areas.Identity.Data;

namespace E_project2025.Models
{
    public class SeminarViewModel
    {
        public seminar Seminar { get; set; }
        public List<E_project2025User> Users { get; set; }
    }
}
