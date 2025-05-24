using JobTrackerApp.Application.Common.Results;
using JobTrackerApp.Domain.Entities;

namespace JobTrackerApp.Application.Interfaces
{
    public interface IJobApplicationService
    {
        Task<ServiceResult<JobApplication?>> GetByIdAsync(int id);  
        Task<ServiceResult<IEnumerable<JobApplication>>> GetAllByUserAsync(int userId); 
        Task<ServiceResult<JobApplication>> CreateAsync(JobApplication application);    
        Task<ServiceResult<JobApplication>> UpdateAsync(JobApplication application);    
        Task<ServiceResult> DeleteAsync(int id);                                 
    }
}
