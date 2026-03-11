using DkHoc.Ports;
using DkHoc.Core.Entity;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessSqlite
{
    public class SqliteCourseRepository : ICourseRepository
    {
        private readonly SqliteData _db;
        public SqliteCourseRepository(SqliteData db) => _db = db;

        public void Create(Course course)
        {
            _db.Courses.Add(course);
        }

        public void Delete(string id)
        {
            var course = _db.Courses.FirstOrDefault(x => x.CourseId == id);
            _db.Courses.Remove(course);
        }

        public Course GetbyId(string id) => _db.Courses.FirstOrDefault(i => i.CourseId == id);
        public Course GetbyName(string name) => _db.Courses.FirstOrDefault(i => i.CourseName == name);
        public IEnumerable<Course> GetAll() => _db.Courses.AsEnumerable();

        public void Update(Course course)
        {
            _db.Courses.Update(course);
        }
    }
}
