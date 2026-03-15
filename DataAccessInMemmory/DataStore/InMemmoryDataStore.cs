using Domain.Core;

namespace DkHoc.DataAccess;

public class InMemmoryDataStore
{
    public List<Student> students = new List<Student>();
    public List<Course> Courses = new List<Course>();
    public List<Registration> Registrations = new List<Registration>();   //Chi de get, ko de set 
}
