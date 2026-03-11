using DkHoc.Core.Entity;
namespace DkHoc.Ports
{
    public interface ICourseRepository
    {
        void Create(Course course);
        void Delete(string id);
        IEnumerable<Course> GetAll();
        Course GetbyId(string id);
        Course GetbyName(string name);
        void Update(Course Course);
    }
}