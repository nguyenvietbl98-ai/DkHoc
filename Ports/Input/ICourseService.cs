using Domain.Core;

namespace Ports.Input;

public interface ICourseService
{
    void Create(string couserName, int credit, string teacherName, int thu, int SiSoMax);
    IEnumerable<Course> GetAllCourse();
    Course GetById(string id);
    Course GetByName(string name);
    void Delete(string id);
}