
using DkHoc.Ports;
using DkHoc.Core.Entity;

namespace BussinessService;

public class StudentService
{
    private readonly IStudentRepository _repo;
    private readonly IUnitOfWork _uow;
    public StudentService(IStudentRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public void Create(int id, string name, string @class)
    {
        Student student = new()
        {
            Id = id,
            Name = name,
            Class = @class,
            Credit = 0
        };
        _repo.Create(student);
        _uow.SaveChange();
    }
    public IEnumerable<Student> GetAllStudent() => _repo.GetAll();
    public Student GetById(int id) => _repo.GetbyId(id);
    public Student GetByName(string name) => _repo.GetbyName(name);
    public void Delete(int id)
    {
        _repo.Delete(id);
        _uow.SaveChange();
    }
}
