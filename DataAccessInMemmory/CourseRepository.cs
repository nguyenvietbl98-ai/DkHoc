using DkHoc.Core.Entity;
using DkHoc.Ports;

namespace DkHoc.DataAccess;

public class CourseRepository(InMemmoryDataStore db) : ICourseRepository
{
    public void Create(Course course)
    {
        db.Courses.Add(course);
    }
    public Course GetbyId(string id) => db.Courses.FirstOrDefault(i => i.CourseId == id);
    public Course GetbyName(string name) => db.Courses.FirstOrDefault(i => i.CourseName == name);
    public IEnumerable<Course> GetAll() => db.Courses.AsReadOnly();

    public void Update(Course Course) { }
    public void Delete(string id)
    {
        var Course = db.Courses.FirstOrDefault(i => i.CourseId == id);
        if (Course != null) db.Courses.Remove(Course);
        else
            throw new Exception("Not found");
    }
}
