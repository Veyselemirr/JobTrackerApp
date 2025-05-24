using JobTrackerApp.Domain.Entities;

namespace JobTrackerApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);    
        Task<IEnumerable<User>> GetAllAsync(); 
        Task<User> CreateAsync(User user);  
        Task<User> UpdateAsync(User user);  
        Task<bool> DeleteAsync(int id);    
}
