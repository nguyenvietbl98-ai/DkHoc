using Microsoft.EntityFrameworkCore;
using Ports.Input;
using Ports.Output;
using BussinessService;
using DataAccessSqlite;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

var dbPath = Path.Combine(AppContext.BaseDirectory, "dkhoc.db");
builder.Services.AddDbContext<SqliteData>(o => o.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<IStudentRepository, SqliteStudentRepository>();
builder.Services.AddScoped<ICourseRepository, SqliteCourseRepository>();
builder.Services.AddScoped<IRegistrationRepository, SqliteRegistrationRepository>();
builder.Services.AddScoped<IUnitOfWork, SqliteUnitOfWork>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();