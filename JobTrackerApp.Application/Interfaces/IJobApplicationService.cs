using JobTrackerApp.Domain.Entities;

namespace JobTrackerApp.Application.Interfaces
{
    public interface IJobApplicationService
    {
        Task<JobApplication?> GetByIdAsync(int id);  
        Task<IEnumerable<JobApplication>> GetAllByUserAsync(int userId); 
        Task<JobApplication> CreateAsync(JobApplication application);    
        Task<JobApplication> UpdateAsync(JobApplication application);    
        Task<bool> DeleteAsync(int id);                                 
    }
}
