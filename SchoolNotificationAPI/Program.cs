using SchoolNotificationAPI.Infrastructure.Persistence;
using SchoolNotificationAPI.Infrastructure.Repositories;
using SchoolNotificationAPI.Application.Interfaces.Repositories;
using SchoolNotificationAPI.Application.Feature.StudentManagement.Interfaces;
using SchoolNotificationAPI.Application.Feature.StudentManagement.Services;

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