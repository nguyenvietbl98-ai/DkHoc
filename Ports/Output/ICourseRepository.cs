using Domain.Core;
namespace Ports.Output
{
    public interface ICourseRepository
    {
        void Create(Course course);
        void Delete(int id);
        IEnumerable<Course> GetAll();
        Course GetbyId(int id);
        Course GetbyName(string name);
        void Update(Course Course);
    }
}