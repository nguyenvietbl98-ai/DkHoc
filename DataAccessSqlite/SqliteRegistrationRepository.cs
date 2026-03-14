using Ports.Output;
using System;
using Domain.Core;
namespace DataAccessSqlite
{
    public class SqliteRegistrationRepository : IRegistrationRepository
    {
        private readonly SqliteData _db;
        public SqliteRegistrationRepository(SqliteData db) => _db = db;
        public void Create(Registration Registration)
        {
            _db.Registrations.Add(Registration);
        }

        public void Delete(int id)
        {
            var registration = _db.Registrations.FirstOrDefault(x => x.Id == id);
            if (registration != null) _db.Registrations.Remove(registration);
        }

        public Registration GetbyId(int id) => _db.Registrations.FirstOrDefault(i => i.Id == id);
        public IEnumerable<Registration> GetByStudentId(int id) => _db.Registrations.Where(re => re.StudentId == id).ToList();
        public IEnumerable<Registration> GetAll() => _db.Registrations.AsEnumerable();

        public void Update(Registration Registration)
        {
            _db.Registrations.Update(Registration);
        }
    }
}
