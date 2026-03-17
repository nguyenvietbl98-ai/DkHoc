using Ports.Input;
using Domain.Core;
using Ports.Output;

namespace BussinessService;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _course;
    private readonly IUnitOfWork _uow;

    public CourseService(ICourseRepository courseRepository, IUnitOfWork uow)
    {
        _course = courseRepository;
        _uow = uow;
    }

    public void Create(string couserName, int credit, string teacherName, int thu, int SiSoMax)
    {
      
        var course = new Course(couserName, credit, teacherName, thu, SiSoMax);

        _course.Create(course);
        _uow.SaveChange();
    }

    public IEnumerable<Course> GetAllCourse() => _course.GetAll();
    public Course GetById(int id) => _course.GetbyId(id);
    public Course GetByName(string name) => _course.GetbyName(name);
    public void Delete(int id)
    {
        _course.Delete(id);
        _uow.SaveChange();
    }

    public void Update(int id, string couserName, int credit, string teacherName, int thu, int SiSoMax)
    {
        var course = _course.GetbyId(id);
        if (course == null) throw new Exception("Khong tim thay lop hoc");

        course.CourseName = couserName;
        course.Credit = credit;
        course.TeacherName = teacherName;
        course.Thu = thu;
        course.SSmax = SiSoMax;

        _course.Update(course);
        _uow.SaveChange();
    }
}