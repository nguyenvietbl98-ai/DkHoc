using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ports.Input;
using Ports.Output;
using System.Linq;

namespace WebUI;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        // register dependencies
        builder.Services.AddSingleton<DkHoc.DataAccess.InMemmoryDataStore>();

        builder.Services.AddScoped<Ports.Output.IStudentRepository, DkHoc.DataAccess.StudentRepository>();
        builder.Services.AddScoped<Ports.Output.ICourseRepository, DkHoc.DataAccess.CourseRepository>();
        builder.Services.AddScoped<Ports.Output.IRegistrationRepository, DkHoc.DataAccess.RegistrationRepository>();

        builder.Services.AddScoped<Ports.Output.IUnitOfWork, DataAccessInMemmory.UnitOfWork>();

        builder.Services.AddScoped<IStudentService, BussinessService.StudentService>();
        builder.Services.AddScoped<ICourseService, BussinessService.CourseService>();
        builder.Services.AddScoped<IRegistrationService, BussinessService.RegistrationService>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Seed initial data for in-memory datastore so Courses and Registrations appear in UI
        using (var scope = app.Services.CreateScope())
        {
            var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();
            var courseService = scope.ServiceProvider.GetRequiredService<ICourseService>();
            var registrationService = scope.ServiceProvider.GetRequiredService<IRegistrationService>();

            var students = studentService.GetAllStudent();
            var courses = courseService.GetAllCourse();
            var regs = registrationService.GetAllRegistration();

            if (!students.Any())
            {
                studentService.Create("Viet", "CNTT");
                studentService.Create("Son", "KTCN");
                studentService.Create("Hoa", "T");
            }

            if (!courses.Any())
            {
                courseService.Create("CNTT", 3, "Hoang", 3, 40);
                courseService.Create("Marketing", 4, "Thu", 4, 20);
                courseService.Create("Tech", 5, "Mibh", 3, 10);
            }

            if (!regs.Any())
            {
                var stud = studentService.GetAllStudent().FirstOrDefault();
                var cour = courseService.GetAllCourse().FirstOrDefault();
                if (stud != null && cour != null)
                {
                    try
                    {
                        registrationService.Register(stud.Id, cour.CourseId);
                    }
                    catch { /* ignore registration errors during seed */ }
                }
            }
        }

        app.Run();
    }
}
