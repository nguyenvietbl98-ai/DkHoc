using Ports.Output;
using Domain.Core;

namespace DkHoc.DataAccess;

public class StudentRepository(InMemmoryDataStore db) : IStudentRepository
{
    public void Create(Student student)
    {
        // assign Id if not set
        if (student.Id == 0)
        {
            var max = db.students.Any() ? db.students.Max(s => s.Id) : 0;
            student.Id = max + 1;
        }
        db.students.Add(student);
    }
    public Student GetbyId(int id) => db.students.FirstOrDefault(i => i.Id == id);
    public Student GetbyName(string name) => db.students.FirstOrDefault(i => i.Name == name);
    public IEnumerable<Student> GetAll() => db.students.AsReadOnly();

    public void Update(Student student) { }
    public void Delete(int id)
    {
        var student = db.students.FirstOrDefault(i => i.Id == id);
        if (student != null) db.students.Remove(student);
        else
            throw new Exception("Not found");
    }


}
