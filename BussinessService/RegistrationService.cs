using Ports.Input;
using Domain.Core;
using Ports.Output;

namespace BussinessService;

public class RegistrationService : IRegistrationService
{
    private readonly IStudentRepository _student;
    private readonly ICourseRepository _course;
    private readonly IRegistrationRepository _registration;
    private readonly IUnitOfWork _uow;

    public RegistrationService(IStudentRepository student, ICourseRepository course, IRegistrationRepository registration, IUnitOfWork uow)
    {
        _student = student;
        _course = course;
        _registration = registration;
        _uow = uow;
    }

    public void Register(int id, int studentId, string courseId)
    {
        var student = _student.GetbyId(studentId);
        if (student == null) throw new Exception("Ko co sinh vien");

        var course = _course.GetbyId(courseId);
        if (course == null) throw new Exception("Khong co lop");

        if (course.SSmax > 0 && course.SSNow >= course.SSmax) throw new Exception("Da du sinh vien");
        if (student.LichHoc.Contains(course.Thu)) throw new Exception($"Trung lich thu {course.Thu}");
        if (student.Credit + course.Credit >= 15) throw new Exception("Qua tin chi cho phep");

        Registration registration = new Registration()
        {
            Id = id,
            StudentId = studentId,
            CourseId = courseId,
            TimeDangKy = DateTime.Now,
        };

        student.Credit = student.Credit + course.Credit;
        course.SSNow++;
        student.LichHoc.Add(course.Thu);

        _registration.Create(registration);
        _uow.SaveChange();
    }

    public void CancelRegister(int registrationId)
    {
        var registration = _registration.GetbyId(registrationId);
        if (registration == null) throw new Exception("Khong co ban dang ky");

        var course = _course.GetbyId(registration.CourseId);
        var student = _student.GetbyId(registration.StudentId);

        course.SSNow--;
        student.Credit = student.Credit - course.Credit;
        student.LichHoc.Remove(course.Thu);

        _uow.SaveChange();
    }

    public IEnumerable<Registration> GetRegistrationsByStudentId(int studentId) => _registration.GetByStudentId(studentId);
    public IEnumerable<Registration> GetAllRegistration() => _registration.GetAll();
    public Registration GetById(int id) => _registration.GetbyId(id);
}