using BussinessService;
using DataAccessInMemmory;
using DataAccessSqlite;
using DkHoc.DataAccess;
using Ports.Output;
using DkHoc.UserInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ports.Input;
namespace DkHoc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sp = SetupSqlite();
            Seed(sp);
            var ui = sp.GetRequiredService<ConsoleUI>();
            ui.Start();

        }
        //static ServiceProvider SetupInMemmory()
        //{
        //    var service = new ServiceCollection();

        //    service.AddSingleton<InMemmoryDataStore>();

        //    service.AddSingleton<IStudentRepository, StudentRepository>();
        //    service.AddSingleton<ICourseRepository, CourseRepository>();
        //    service.AddSingleton<IRegistrationRepository, RegistrationRepository>();
        //    service.AddSingleton<IUnitOfWork,UnitOfWork>();

        //    service.AddScoped<StudentService>();
        //    service.AddScoped<CourseService>();
        //    service.AddScoped<RegistrationService>();

        //    service.AddScoped<ConsoleUI>();

        //    return service.BuildServiceProvider();
        //}
        static ServiceProvider SetupSqlite()
        {
            var service = new ServiceCollection();

            
            service.AddScoped<IStudentRepository, SqliteStudentRepository>();
            service.AddScoped<ICourseRepository, SqliteCourseRepository>();
            service.AddScoped<IRegistrationRepository, SqliteRegistrationRepository>();
            service.AddScoped<IUnitOfWork, SqliteUnitOfWork>();

            
            var dbPath = Path.Combine(AppContext.BaseDirectory, "dkhoc.db");
            var conStri = $"Data Source={dbPath}";
            service.AddDbContext<SqliteData>(o => o.UseSqlite(conStri));

         
            service.AddScoped<IStudentService, StudentService>();
            service.AddScoped<ICourseService, CourseService>();
            service.AddScoped<IRegistrationService, RegistrationService>();

           
            service.AddScoped<ConsoleUI>();

            var sp = service.BuildServiceProvider();

           
            using (var scope = sp.CreateScope())
            {
              
                var db = scope.ServiceProvider.GetRequiredService<SqliteData>();
                db.Database.EnsureCreated();
            }
            return sp;
        }
        static void Seed(ServiceProvider sp)
        {
            var scope = sp.CreateScope();

            
            var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();
            var courseService = scope.ServiceProvider.GetRequiredService<ICourseService>();
            var registrationService = scope.ServiceProvider.GetRequiredService<IRegistrationService>();

            var student = studentService.GetAllStudent();
            var course = courseService.GetAllCourse();
            var registration = registrationService.GetAllRegistration();

            if (!student.Any())
            {
                studentService.Create("Viet", "CNTT");
                studentService.Create("Son", "KTCN");
                studentService.Create("hòa", "T");
            }
            if (!course.Any())
            {
                courseService.Create("CNTT", 3, "Hoang", 3, 40);
                courseService.Create("Marketing", 4, "Thu", 4, 20);
                courseService.Create("Tech", 5, "Mibh", 3, 10);
            }
            if (!registration.Any())
            {
              
                var studentsAfter = studentService.GetAllStudent().ToList();
                var coursesAfter = courseService.GetAllCourse().ToList();
                if (studentsAfter.Any() && coursesAfter.Any())
                {
                    var studentId = studentsAfter.First().Id;
                    var courseId = coursesAfter.First().CourseId;
                    registrationService.Register(studentId, courseId);
                }
            }
        }

    }
}
