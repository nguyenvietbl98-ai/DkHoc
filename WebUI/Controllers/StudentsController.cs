using Microsoft.AspNetCore.Mvc;
using Ports.Input;
using Domain.Core;

namespace WebUI.Controllers;

public class StudentsController : Controller
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public IActionResult Index()
    {
        var students = _studentService.GetAllStudent();
        return View(students);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(string name, string @class)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(@class))
        {
            ModelState.AddModelError(string.Empty, "Name and Class are required");
            return View();
        }

        _studentService.Create(name, @class);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var student = _studentService.GetById(id);
        if (student == null) return NotFound();
        return View(student);
    }

    [HttpPost]
    public IActionResult Edit(int id, string name, string @class)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(@class))
        {
            ModelState.AddModelError(string.Empty, "Name and Class are required");
            var s = _studentService.GetById(id);
            return View(s);
        }

        _studentService.Update(id, name, @class);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        _studentService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
