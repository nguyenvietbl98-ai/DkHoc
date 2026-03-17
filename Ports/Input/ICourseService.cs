using Domain.Core;

namespace Ports.Input;

public interface ICourseService
{
    void Create(string couserName, int credit, string teacherName, int thu, int SiSoMax);
    void Update(int id, string couserName, int credit, string teacherName, int thu, int SiSoMax);
    IEnumerable<Course> GetAllCourse();
    Course GetById(int id);
    Course GetByName(string name);
    void Delete(int id);
}