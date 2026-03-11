using DkHoc.Core.Entity;
using DkHoc.Ports;

namespace DkHoc.DataAccess;

public class RegistrationRepository(InMemmoryDataStore db) : IRegistrationRepository
{
    public void Create(Registration Registration)
    {
        db.Registrations.Add(Registration);
    }
    public Registration GetbyId(int id) => db.Registrations.FirstOrDefault(i => i.Id == id);
    public IEnumerable<Registration> GetByStudentId(int id) => db.Registrations.Where(re => re.StudentId == id).ToList();
    public IEnumerable<Registration> GetAll() => db.Registrations.AsReadOnly();

    public void Update(Registration Registration) { }
    public void Delete(int id)
    {
        var Registration = db.Registrations.FirstOrDefault(i => i.Id == id);
        if (Registration != null) db.Registrations.Remove(Registration);
        else
            throw new Exception("Not found");
    }
}
