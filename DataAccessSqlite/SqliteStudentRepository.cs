
using Ports.Output;
using Domain.Core;

namespace DataAccessSqlite
{
    public class SqliteStudentRepository : IStudentRepository
    {
        private readonly SqliteData _db;
        public SqliteStudentRepository(SqliteData db)=> _db = db;
        public void Create(Student student)
        {
           _db.Students.Add(student);
        }

        public void Delete(int id)
        {
          var student = _db.Students.FirstOrDefault(x => x.Id == id);
            _db.Students.Remove(student);
        }

        public Student GetbyId(int id) => _db.Students.FirstOrDefault(i => i.Id == id);
        public Student GetbyName(string name) => _db.Students.FirstOrDefault(i => i.Name == name);
        public IEnumerable<Student> GetAll() => _db.Students.AsEnumerable();

        public void Update(Student student)
        {
            _db.Students.Update(student);
        }
    }
}
