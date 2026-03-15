namespace Domain.Core;

public class Student
{
    private int _id;
    private string _name;
    private string _class;
    private int _credit;
    private List<int> _lichHoc = new();

    public Student(string name, string lop)
    {
        Name = name;
        Class = lop;
        Credit = 0;
    }

    protected Student() { }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "Ten khong duoc rong");

            _name = value;
        }
    }

    public string Class
    {
        get => _class;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "Lop khong duoc rong");

            _class = value;
        }
    }

    public int Credit
    {
        get => _credit;
        set
        {
            if (value < 0 || value > 15)
                throw new ArgumentOutOfRangeException(nameof(value), "Vuot qua so tin chi");

            _credit = value;
        }
    }

    public List<int> LichHoc
    {
        get => _lichHoc;
        set => _lichHoc = value ?? new List<int>();
    }

    public void AddSchedule(int thu)
    {
        if (_lichHoc.Contains(thu))
            throw new Exception("Trung lich hoc");

        _lichHoc.Add(thu);
    }

    public void RemoveSchedule(int thu)
    {
        _lichHoc.Remove(thu);
    }
}