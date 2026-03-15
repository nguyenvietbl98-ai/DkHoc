namespace Domain.Core;

public class Registration
{
    private int _id;
    private int _studentId;
    private string _courseId;
    private DateTime _timeDangKy;

    public Registration(int studentId, string courseId, DateTime timeDangKy)
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

    public string CourseId
    {
        get => _courseId;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "CourseId khong duoc rong");

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