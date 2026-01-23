using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_project2025.Models
{
    public class SeminarRegistration
    {
        public int Id { get; set; }
        public int SeminarId { get; set; }
        public string UserId { get; set; }
        public DateTime RegisteredOn { get; set; }
    }
}
