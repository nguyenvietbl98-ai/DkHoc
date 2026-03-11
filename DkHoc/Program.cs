using BussinessService;
using DkHoc.Ports;
using DataAccessInMemmory;
using DataAccessSqlite;
using DkHoc.DataAccess;
using DkHoc.UserInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            service.AddScoped<ICourseRepository,SqliteCourseRepository>();
            service.AddScoped<IRegistrationRepository, SqliteRegistrationRepository>();
            service.AddScoped<IUnitOfWork, SqliteUnitOfWork>();

            var dbPath = Path.Combine(AppContext.BaseDirectory, "dkhoc.db");
            var conStri = $"Data Source={dbPath}";
            service.AddDbContext<SqliteData>(o=> o.UseSqlite(conStri));

            service.AddScoped<StudentService>();
            service.AddScoped<CourseService>();
            service.AddScoped<RegistrationService>();

            service.AddScoped<ConsoleUI>();

            var sp = service.BuildServiceProvider();

            using(var scope = sp.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SqliteData>();
                db.Database.EnsureCreated();
            }
            return sp;
        }
        static void Seed(ServiceProvider sp)
        {
            var scope = sp.CreateScope();
            var studentService = scope.ServiceProvider.GetRequiredService<StudentService>();
            var courseService = scope.ServiceProvider.GetRequiredService<CourseService>();
            var registrationService = scope.ServiceProvider.GetRequiredService<RegistrationService>();

            var student = studentService.GetAllStudent();
            var course = courseService.GetAllCourse();
            var registration = registrationService.GetAllRegistration();

            if (!student.Any())
            {
                studentService.Create(1, "Nam", "CNTT");
                studentService.Create(2, "Duc", "KTCN");
                studentService.Create(3, "Aanh", "T");
            }
            if (!course.Any())
            {
                courseService.Create("1", "CNTT", 3, "Hoang", 3, 40);
                courseService.Create("2", "Marketing", 4, "Thu", 4, 20);
                courseService.Create("3", "Tech", 5, "Mibh", 3, 10);
            }
            if (!registration.Any())
            {
                registrationService.Register(0, 2, "2");
            }
        }

    }
}
