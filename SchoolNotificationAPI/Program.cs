using SchoolNotificationAPI.Application.Feature.Notifications.Interfaces;
using SchoolNotificationAPI.Application.Feature.Notifications.Services;
using SchoolNotificationAPI.Application.Feature.Students.Interfaces;
using SchoolNotificationAPI.Application.Feature.Students.Services;
using SchoolNotificationAPI.Infrastructure.Persistence;
using SchoolNotificationAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<
    IStudentRepository,
    StudentRepository>();

builder.Services.AddScoped<
    IStudentService,
    StudentService>();

builder.Services.AddScoped<
    INotificationRepository,
    NotificationRepository>();

builder.Services.AddScoped<
    INotificationService,
    NotificationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();