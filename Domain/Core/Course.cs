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

    public string CourseId
    {
        get => _courseId; set => _courseId = value;
    }
    public string CourseName
    {
        get => _courseName; set
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            _courseName = value;
        }
    }
    public int Credit
    {
        get => _credit; set
        {
            if (value <= 0 || value > 5)
            {
                throw new ArgumentOutOfRangeException();
            }
            _credit = value;
        }
    }
    public string TeacherName
    {
        get => _teacherName; set
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            _teacherName = value;
        }
    }
    public int Thu
    {
        get => _dateOfWe; set
        {
            if (value <= 1 || value > 8)
            {
                throw new ArgumentOutOfRangeException("Thu sai");
            }
            _dateOfWe = value;
        }
    }
    public int SSmax
    {
        get => _ssmax; set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException("Nhieu hon 0 va it hon 100 sv");
            }
            _ssmax = value;
        }
    }
    public int SSNow
    {
        get => _ssnow; set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException("Qua so luot dang ky");
            }
            _ssnow = value;
        }
    }
}