using System;
using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DataAccessSqlite;
using Ports.Output;
using BussinessService;
using Ports.Input;

namespace WpfUI2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddScoped<IStudentRepository, SqliteStudentRepository>();
            services.AddScoped<ICourseRepository, SqliteCourseRepository>();
            services.AddScoped<IRegistrationRepository, SqliteRegistrationRepository>();
            services.AddScoped<IUnitOfWork, SqliteUnitOfWork>();

            var dbPath = Path.Combine(AppContext.BaseDirectory, "dkhoc.db");
            var conStr = $"Data Source={dbPath}";
            services.AddDbContext<SqliteData>(o => o.UseSqlite(conStr));

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IRegistrationService, RegistrationService>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();


            using (var scope = _serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SqliteData>();
                db.Database.EnsureCreated();
            }


            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            if (_serviceProvider is not null)
            {
                _serviceProvider.Dispose();
            }
        }
    }
}
