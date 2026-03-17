using Microsoft.AspNetCore.Mvc;
using Ports.Input;

namespace WebUI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;


        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var students = _studentService.GetAllStudent();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string className)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(className))
                {
                    ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
                    return View(); 
                }

               
                _studentService.Create(name, className);

               
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }


        [HttpGet]
        public IActionResult Details(int? id)
        {
           
            if (id == null) return View();

            try
            {
                var student = _studentService.GetById(id.Value);
                if (student == null) ViewBag.Error = "Không tìm thấy sinh viên!";
                return View(student);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi: " + ex.Message;
                return View();
            }
        }
    }
}