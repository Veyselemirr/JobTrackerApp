using JobTrackerApp.Application.Interfaces;
using JobTrackerApp.Infrastructure.Data;
using JobTrackerApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS için politika adý
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

// 1. CORS servisini ekle
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000") // Next.js uygulamanýn adresi
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                          // Eðer farklý portlarda veya production'da baþka adreslere de izin vermek istersen:
                          // policy.WithOrigins("http://localhost:3000", "https://senin-production-adresin.com")
                          // Eðer tüm origin'lere (kaynaklara) izin vermek istersen (geliþtirme için olabilir ama production'da dikkatli ol):
                          // policy.AllowAnyOrigin()
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();