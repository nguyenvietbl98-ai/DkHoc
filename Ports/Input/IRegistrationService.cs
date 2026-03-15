using Domain.Core;

namespace Ports.Input;

public interface IRegistrationService
{
    void Register(int studentId, string courseId);
    void CancelRegister(int registrationId);
    IEnumerable<Registration> GetRegistrationsByStudentId(int studentId);
    IEnumerable<Registration> GetAllRegistration();
    Registration GetById(int id);
}