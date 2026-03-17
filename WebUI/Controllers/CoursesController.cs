using Microsoft.AspNetCore.Mvc;
using Ports.Input;
using Domain.Core;

namespace WebUI.Controllers;

public class CoursesController : Controller
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public IActionResult Index()
    {
        var courses = _courseService.GetAllCourse();
        return View(courses);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(string courseName, int credit, string teacherName, int thu, int ssMax)
    {
        if (string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(teacherName))
        {
            ModelState.AddModelError(string.Empty, "CourseName and TeacherName are required");
            return View();
        }

        _courseService.Create(courseName, credit, teacherName, thu, ssMax);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var course = _courseService.GetById(id);
        if (course == null) return NotFound();
        return View(course);
    }

    [HttpPost]
    public IActionResult Edit(int id, string courseName, int credit, string teacherName, int thu, int ssMax)
    {
        if (string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(teacherName))
        {
            ModelState.AddModelError(string.Empty, "CourseName and TeacherName are required");
            var c = _courseService.GetById(id);
            return View(c);
        }

        _courseService.Update(id, courseName, credit, teacherName, thu, ssMax);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        _courseService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
