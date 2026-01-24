using E_project2025.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_project2025.Models
{
    public class SeminarRegistration
    {
        public int Id { get; set; }
        
        public int SeminarId { get; set; }
        [ForeignKey("SeminarId")]
        public virtual seminar seminar { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual E_project2025User users { get; set; }
        public DateTime RegisteredOn { get; set; }
    }
}
