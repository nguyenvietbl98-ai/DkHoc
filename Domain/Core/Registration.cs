namespace Domain.Core;

public class Registration
{
    private int _id;
    private int _studentId;
    private string _courseId;
    private DateTime _timeDangKy;
    public int Id
    {
        get => _id;set=> _id=value;
    }
    public int StudentId
    {
        get => _studentId; set
        {
           if(string.IsNullOrEmpty(nameof(value))) throw new Exception(nameof(value));
           _studentId=value;
        }
    }
    public string CourseId
    {
        get => _courseId; set
        {
           if(string.IsNullOrEmpty(nameof(value))) throw new Exception(nameof(value));
           _courseId=value;
        }
    }
    public DateTime TimeDangKy
    {
        get => _timeDangKy; set
        {
            if (string.IsNullOrEmpty(nameof(value))) throw new Exception(nameof(value));
            _timeDangKy=value;
        }
    }

}
