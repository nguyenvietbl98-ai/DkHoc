using Microsoft.AspNetCore.Mvc;
using Ports.Input;
using Domain.Core;

namespace WebUI.Controllers;

public class RegistrationsController : Controller
{
    private readonly IRegistrationService _regService;
    private readonly IStudentService _studentService;
    private readonly ICourseService _courseService;

    public RegistrationsController(IRegistrationService regService, IStudentService studentService, ICourseService courseService)
    {
        _regService = regService;
        _studentService = studentService;
        _courseService = courseService;
    }

    public IActionResult Index()
    {
        var regs = _regService.GetAllRegistration();
        return View(regs);
    }

    public IActionResult Create()
    {
        ViewBag.Students = _studentService.GetAllStudent();
        ViewBag.Courses = _courseService.GetAllCourse();
        return View();
    }

    [HttpPost]
    public IActionResult Create(int studentId, int courseId)
    {
        try
        {
            _regService.Register(studentId, courseId);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            ViewBag.Students = _studentService.GetAllStudent();
            ViewBag.Courses = _courseService.GetAllCourse();
            return View();
        }
    }

    [HttpPost]
    public IActionResult Cancel(int id)
    {
        _regService.CancelRegister(id);
        return RedirectToAction(nameof(Index));
    }
}
