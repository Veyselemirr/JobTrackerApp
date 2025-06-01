using JobTrackerApp.Application.Interfaces;
using JobTrackerApp.Domain.Entities;
using JobTrackerApp.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JobTrackerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationService _jobService;

        public JobApplicationController(IJobApplicationService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _jobService.GetByIdAsync(id);
            if (result.IsFail)
                return StatusCode((int)result.Status, result.ErrorMessage);

            var app = result.Data!;

            var dto = new JobApplicationResponseDto
            {
                Id = app.Id,
                CompanyName = app.CompanyName,
                Position = app.Position,
                AppliedDate = app.AppliedDate,
                Status = (int)app.Status,
                Notes = app.Notes,
                UserId = app.UserId
            };

            return Ok(dto);
        }


        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllByUser(int userId)
        {
            var result = await _jobService.GetAllByUserAsync(userId);
            return StatusCode((int)result.Status, result.Data);
        }

        [HttpPost]

public async Task<IActionResult> Create(JobApplicationCreateDto dto)
{
    var application = new JobApplication
    {
        CompanyName = dto.CompanyName,
        Position = dto.Position,
        AppliedDate = dto.AppliedDate,
        Status = (ApplicationStatus)dto.Status,
        Notes = dto.Notes,
        UserId = dto.UserId,
        Location = dto.Location,
        WorkModel = dto.WorkModel,
        InterviewDate = dto.InterviewDate,       
    };

    var result = await _jobService.CreateAsync(application);
    return StatusCode((int)result.Status, result.Data);
}


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobApplicationUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID uyuşmazlığı");

            var application = new JobApplication
            {
                Id = dto.Id,
                CompanyName = dto.CompanyName,
                Position = dto.Position,
                AppliedDate = dto.AppliedDate,
                Status = (ApplicationStatus)dto.Status,
                Notes = dto.Notes,
                UserId = dto.UserId,
                Location = dto.Location,
                WorkModel = dto.WorkModel,
                InterviewDate= dto.InterviewDate,
            };

            var result = await _jobService.UpdateAsync(application);
            return StatusCode((int)result.Status, result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _jobService.DeleteAsync(id);
            return StatusCode((int)result.Status, result);
        }
    }
}
