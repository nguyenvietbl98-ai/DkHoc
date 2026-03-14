using Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace DataAccessSqlite
{
    public class SqliteData:DbContext
    {
        public SqliteData(DbContextOptions<SqliteData> options) : base(options){}

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Registration> Registrations { get; set; }
    }
}
