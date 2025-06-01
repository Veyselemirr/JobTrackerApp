using JobTrackerApp.Domain.Enums;

namespace JobTrackerApp.Domain.Entities
{
    public class JobApplication
    {
        public int Id { get; set; }  
        public string CompanyName { get; set; } = null!;  
        public string Position { get; set; } = null!; 
        public DateTime AppliedDate { get; set; } 
        public ApplicationStatus Status { get; set; }
        public string? Location { get; set; }
        public string? WorkModel { get; set; }
        public string? Notes { get; set; } 
        public int UserId { get; set; }
        public DateTime? InterviewDate { get; set; }
        public User User { get; set; } = null!; 
    }
}
