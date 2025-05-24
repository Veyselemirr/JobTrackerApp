namespace JobTrackerApp.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }  
        public string Name { get; set; } = null!; 
        public string Surname { get; set; } = null!; 

        public string Email { get; set; } = null!;  

        public string PasswordHash { get; set; } = null!; 

        public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
