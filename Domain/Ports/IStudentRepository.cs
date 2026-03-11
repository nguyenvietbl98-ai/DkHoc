using DkHoc.Core.Entity;

namespace DkHoc.Ports
{
    public interface IStudentRepository
    {
        void Create(Student student);
        void Delete(int id);
        IEnumerable<Student> GetAll();
        Student GetbyId(int id);
        Student GetbyName(string name);
        void Update(Student student);
    }
}