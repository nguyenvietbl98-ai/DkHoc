namespace Domain.Core;

public class Registration
{
    private int _id;
    private int _studentId;
    private int _courseId;
    private DateTime _timeDangKy;

    public Registration(int studentId, int courseId, DateTime timeDangKy)
    {
        StudentId = studentId;
        CourseId = courseId;
        TimeDangKy = timeDangKy;
    }

    protected Registration() { }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public int StudentId
    {
        get => _studentId;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "StudentId khong hop le");

            _studentId = value;
        }
    }

    public int CourseId
    {
        get => _courseId;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "CourseId khong hop le");

            _courseId = value;
        }
    }

    public DateTime TimeDangKy
    {
        get => _timeDangKy;
        set
        {
            if (value == default)
                throw new ArgumentException("Thoi gian dang ky khong hop le");

            _timeDangKy = value;
        }
    }
}