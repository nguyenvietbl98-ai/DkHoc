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

            // 1. Map Output Ports -> Secondary Adapters (DataAccess)
            service.AddScoped<IStudentRepository, SqliteStudentRepository>();
            service.AddScoped<ICourseRepository, SqliteCourseRepository>();
            service.AddScoped<IRegistrationRepository, SqliteRegistrationRepository>();
            service.AddScoped<IUnitOfWork, SqliteUnitOfWork>();

            // 2. ĐĂNG KÝ SQLITE DATA (Chính là đoạn code đang bị thiếu gây ra lỗi của bạn)
            var dbPath = Path.Combine(AppContext.BaseDirectory, "dkhoc.db");
            var conStri = $"Data Source={dbPath}";
            service.AddDbContext<SqliteData>(o => o.UseSqlite(conStri));

            // 3. Map Input Ports -> Core Use Cases (BussinessService)
            service.AddScoped<IStudentService, StudentService>();
            service.AddScoped<ICourseService, CourseService>();
            service.AddScoped<IRegistrationService, RegistrationService>();

            // 4. Đăng ký UI
            service.AddScoped<ConsoleUI>();

            var sp = service.BuildServiceProvider();

            // 5. Khởi tạo Database
            using (var scope = sp.CreateScope())
            {
                // Nó sẽ bị lỗi ở dòng dưới này nếu phần 2 (AddDbContext) bị thiếu
                var db = scope.ServiceProvider.GetRequiredService<SqliteData>();
                db.Database.EnsureCreated();
            }
            return sp;
        }
        static void Seed(ServiceProvider sp)
        {
            var scope = sp.CreateScope();

            // Yêu cầu (GetRequiredService) bằng Interface thay vì Class cụ thể
            var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();
            var courseService = scope.ServiceProvider.GetRequiredService<ICourseService>();
            var registrationService = scope.ServiceProvider.GetRequiredService<IRegistrationService>();

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
