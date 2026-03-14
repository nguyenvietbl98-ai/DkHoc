using Domain.Core;

namespace DkHoc.DataAccess;

public class InMemmoryDataStore
{
    public List<Student> students = [];
    public List<Course> Courses = [];
    public List<Registration> Registrations = [];   //Chi de get, ko de set 
}
