using Domain.Core; // Nhớ using đúng namespace chứa class Student của bạn

namespace Ports.Input;

public interface IStudentService
{
    void Create(int id, string name, string @class);
    IEnumerable<Student> GetAllStudent();
    Student GetById(int id);
    Student GetByName(string name);
    void Delete(int id);
}