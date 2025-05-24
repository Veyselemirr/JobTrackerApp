using JobTrackerApp.Application.Common.Results;
using JobTrackerApp.Application.Interfaces;
using JobTrackerApp.Domain.Entities;
using JobTrackerApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace JobTrackerApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<User>> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return ServiceResult<User>.Fail("Kullanıcı bulunamadı", HttpStatusCode.NotFound);

            return ServiceResult<User>.Success(user);
        }

        public async Task<ServiceResult<IEnumerable<User>>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return ServiceResult<IEnumerable<User>>.Success(users);
        }

        public async Task<ServiceResult<User>> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return ServiceResult<User>.Success(user, HttpStatusCode.Created);
        }

        public async Task<ServiceResult<User>> UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
                return ServiceResult<User>.Fail("Güncellenecek kullanıcı bulunamadı", HttpStatusCode.NotFound);

            existingUser.Name = user.Name;
            existingUser.Surname = user.Surname;
            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return ServiceResult<User>.Success(existingUser);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return ServiceResult.Fail("Silinecek kullanıcı bulunamadı", HttpStatusCode.NotFound);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
