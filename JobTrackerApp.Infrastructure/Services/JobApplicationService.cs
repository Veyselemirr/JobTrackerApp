using JobTrackerApp.Application.Common.Results;
using JobTrackerApp.Application.Interfaces;
using JobTrackerApp.Domain.Entities;
using JobTrackerApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace JobTrackerApp.Infrastructure.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly ApplicationDbContext _context;

        public JobApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ID ile başvuru getir
        public async Task<ServiceResult<JobApplication>> GetByIdAsync(int id)
        {
            var app = await _context.JobApplications.FindAsync(id);
            if (app == null)
                return ServiceResult<JobApplication>.Fail("Başvuru bulunamadı", HttpStatusCode.NotFound);

            return ServiceResult<JobApplication>.Success(app);
        }

        // Kullanıcıya ait başvuruları getir
        public async Task<ServiceResult<IEnumerable<JobApplication>>> GetAllByUserAsync(int userId)
        {
            var apps = await _context.JobApplications
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return ServiceResult<IEnumerable<JobApplication>>.Success(apps);
        }

        // Yeni başvuru oluştur
        public async Task<ServiceResult<JobApplication>> CreateAsync(JobApplication application)
        {
            await _context.JobApplications.AddAsync(application);
            await _context.SaveChangesAsync();

            return ServiceResult<JobApplication>.Success(application, HttpStatusCode.Created);
        }

        // Başvuru güncelle
        public async Task<ServiceResult<JobApplication>> UpdateAsync(JobApplication application)
        {
            var existing = await _context.JobApplications.FindAsync(application.Id);
            if (existing == null)
                return ServiceResult<JobApplication>.Fail("Güncellenecek başvuru bulunamadı", HttpStatusCode.NotFound);

            // Güncellenecek alanlar
            existing.CompanyName = application.CompanyName;
            existing.Position = application.Position;
            existing.AppliedDate = application.AppliedDate;
            existing.Status = application.Status;
            existing.Notes = application.Notes;
            existing.WorkModel=application.WorkModel;
            existing.Location = application.Location;

            _context.JobApplications.Update(existing);
            await _context.SaveChangesAsync();

            return ServiceResult<JobApplication>.Success(existing);
        }

        // Başvuru sil
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var app = await _context.JobApplications.FindAsync(id);
            if (app == null)
                return ServiceResult.Fail("Silinecek başvuru bulunamadı", HttpStatusCode.NotFound);

            _context.JobApplications.Remove(app);
            await _context.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
