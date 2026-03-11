using DkHoc.Core.Entity;

namespace DkHoc.Ports
{
    public interface IRegistrationRepository
    {
        void Create(Registration Registration);
        void Delete(int id);
        IEnumerable<Registration> GetAll();
        Registration GetbyId(int id);
        IEnumerable<Registration> GetByStudentId(int id);
        void Update(Registration Registration);
    }
}