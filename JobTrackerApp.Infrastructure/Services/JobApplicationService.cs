using JobTrackerApp.Application.Interfaces;
using JobTrackerApp.Domain.Entities;
using JobTrackerApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerApp.Infrastructure.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly ApplicationDbContext _context;

        // Constructor ile DbContext enjekte ediliyor (DI)
        public JobApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Başvuru detayını getir
        public async Task<JobApplication?> GetByIdAsync(int id)
        {
            return await _context.JobApplications.FindAsync(id);
        }

        // Kullanıcıya ait başvuruları getir
        public async Task<IEnumerable<JobApplication>> GetAllByUserAsync(int userId)
        {
            return await _context.JobApplications
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        // Yeni başvuru oluştur
        public async Task<JobApplication> CreateAsync(JobApplication application)
        {
            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();
            return application;
        }

        // Başvuruyu güncelle
        public async Task<JobApplication> UpdateAsync(JobApplication application)
        {
            _context.JobApplications.Update(application);
            await _context.SaveChangesAsync();
            return application;
        }

        // Başvuruyu sil
        public async Task<bool> DeleteAsync(int id)
        {
            var app = await _context.JobApplications.FindAsync(id);
            if (app == null) return false;

            _context.JobApplications.Remove(app);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
