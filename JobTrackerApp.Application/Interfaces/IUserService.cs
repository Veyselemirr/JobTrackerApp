using JobTrackerApp.Application.Common.Results;
using JobTrackerApp.Domain.Entities;

namespace JobTrackerApp.Application.Interfaces;

public interface IUserService
{
    Task<ServiceResult<User>> GetByIdAsync(int id);
    Task<ServiceResult<IEnumerable<User>>> GetAllAsync();
    Task<ServiceResult<User>> CreateAsync(User user);
    Task<ServiceResult<User>> UpdateAsync(User user);
    Task<ServiceResult> DeleteAsync(int id);
}
