using JobTrackerApp.Application.Interfaces;
using JobTrackerApp.Domain.Entities;
using JobTrackerApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        // Constructor injection → SOLID: Dependency Inversion
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Belirli kullanıcıyı getir
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Tüm kullanıcıları getir
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Yeni kullanıcı oluştur
        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);          // DbSet'e kullanıcı ekleniyor
            await _context.SaveChangesAsync(); // Veritabanına kaydediliyor
            return user;
        }

        // Kullanıcıyı güncelle
        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);       // Mevcut kullanıcı güncelleniyor
            await _context.SaveChangesAsync();
            return user;
        }

        // Kullanıcıyı sil
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);       // Kullanıcı siliniyor
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
