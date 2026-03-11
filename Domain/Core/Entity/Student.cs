

namespace DkHoc.Core.Entity;

public class Student
{
	private int _id;
    private string _name;
    private string _class;
    private int _credit;
    private List<int> _lichHoc = new();
    
	public int Id
    {
        get => _id; set => _id = value;
    }
    public string Name
    {
        get => _name; set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            _name = value;
        }
    }
    public string Class
    {
        get => _class; set
        {
            ArgumentNullException.ThrowIfNullOrEmpty(value, nameof(value));
            _class = value;
        }
    }
    public int Credit
    {
        get => _credit; set
        {
            if(value<0 ||value > 15)
            {
             throw new ArgumentOutOfRangeException("Vuot qua tin chi");
            }
            _credit = value;
        }
    }
    public List<int> LichHoc
    {
        get => _lichHoc; set
        {
            _lichHoc = value;
        }
    }

}
