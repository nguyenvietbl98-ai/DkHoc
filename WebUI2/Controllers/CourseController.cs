using Microsoft.AspNetCore.Mvc;
using Ports.Input;

namespace WebUI.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

       
        [HttpGet]
        public IActionResult Index()
        {
            var courses = _courseService.GetAllCourse();
            return View(courses);
        }

        // [2] Create Course (Hiển thị Form)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // [2] Create Course (Xử lý lưu dữ liệu)
        [HttpPost]
        public IActionResult Create(string name, int credit, string teacher, int day, int maxSize)
        {
            try
            {
                _courseService.Create(name, credit, teacher, day, maxSize);
                return RedirectToAction("Index"); // Thêm xong về trang danh sách
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}