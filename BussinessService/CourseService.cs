
using DkHoc.Ports;
using DkHoc.Core.Entity;

namespace BussinessService;

public class CourseService
{
    private readonly ICourseRepository _course;
    private readonly IUnitOfWork _uow;
    public CourseService(ICourseRepository courseRepository,IUnitOfWork uow)
    {
        _course = courseRepository;
        _uow = uow;
    }
    public void Create(string id, string couserName, int credit, string teacherName, int thu, int SiSoMax)
    {
        Course course = new Course()
        {
            CourseId = id,
            CourseName = couserName,
            Credit = credit,
            TeacherName = teacherName,
            Thu = thu,
            SSmax = SiSoMax,
            SSNow = 0
        };
        _course.Create(course);
        _uow.SaveChange();

    }
    public IEnumerable<Course> GetAllCourse() => _course.GetAll();
    public Course GetById(string id) => _course.GetbyId(id);
    public Course GetByName(string name) => _course.GetbyName(name);
    public void Delete(string id)
    {
        _course.Delete(id);
        _uow.SaveChange();
    }
}
