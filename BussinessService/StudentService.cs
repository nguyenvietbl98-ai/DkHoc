using Ports.Input;
using Domain.Core;
using Ports.Output;

namespace BussinessService;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repo;
    private readonly IUnitOfWork _uow;
    public StudentService(IStudentRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public void Create(string name, string @class)
    {
       
        Student student = new Student(name, @class)
        {
            Credit = 0
        };

        _repo.Create(student);
        _uow.SaveChange();
    }
    public void Update(int id, string name, string @class)
    {
        var student = _repo.GetbyId(id);
        if (student == null) throw new Exception("Khong tim thay sinh vien");

        student.Name = name;
        student.Class = @class;

        _repo.Update(student);
        _uow.SaveChange();
    }
    public IEnumerable<Student> GetAllStudent() => _repo.GetAll();
    public Student GetById(int id) => _repo.GetbyId(id);
    public Student GetByName(string name) => _repo.GetbyName(name);
    public void Delete(int id)
    {
        var student = _repo.GetbyId(id);
        if (student == null)
            throw new Exception("Khong tim thay sinh vien");

        _repo.Delete(id);
        _uow.SaveChange();
    }
}
