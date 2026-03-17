namespace Domain.Core;

public class Course
{
    private string _courseId;
    private string _courseName;
    private int _credit;
    private string _teacherName;
    private int _dateOfWe;
    private int _ssmax;
    private int _ssnow;

    public Course(string courseName, int credit, string teacherName, int thu, int ssmax)
    {
        _courseId = Guid.NewGuid().ToString();
        CourseName = courseName;
        Credit = credit;
        TeacherName = teacherName;
        Thu = thu;
        SSmax = ssmax;
        SSNow = 0;
    }

    protected Course() { }

    public string CourseId
    {
        get => _courseId;
        set => _courseId = value;
    }

    public string CourseName
    {
        get => _courseName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "CourseName cannot be null or whitespace");
            _courseName = value;
        }
    }

    public int Credit
    {
        get => _credit;
        set
        {
            if (value <= 0 || value > 5)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Credit must be between 1 and 5");
            _credit = value;
        }
    }

    public string TeacherName
    {
        get => _teacherName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "TeacherName cannot be null or whitespace");
            _teacherName = value;
        }
    }

    public int Thu
    {
        get => _dateOfWe;
        set
        {
            if (value < 2 || value > 8)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Thu must be between 2 and 8");
            _dateOfWe = value;
        }
    }

    public int SSmax
    {
        get => _ssmax;
        set
        {
            if (value <= 0 || value > 100)
                throw new ArgumentOutOfRangeException(nameof(value), value, "SSmax must be between 1 and 100");
            _ssmax = value;
        }
    }

    public int SSNow
    {
        get => _ssnow;
        set
        {
            if (value < 0 || (_ssmax > 0 && value > _ssmax))
                throw new ArgumentOutOfRangeException(nameof(value), value, $"SSNow must be >= 0 and <= SSmax({_ssmax})");
            _ssnow = value;
        }
    }
}