using Microsoft.AspNetCore.Mvc;
using Ports.Input;

namespace WebUI.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        // [8] Show all register (Có ô nhập Student ID)
        [HttpGet]
        public IActionResult Index(int? studentId)
        {
            ViewBag.StudentId = studentId; // Giữ lại ID trên ô tìm kiếm
            if (studentId == null) return View(new List<Domain.Core.Registration>());

            try
            {
                var regs = _registrationService.GetRegistrationsByStudentId(studentId.Value);
                return View(regs);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<Domain.Core.Registration>());
            }
        }

        // [6] Register course (Hiển thị form nhập Student ID và Course ID)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // [6] Register course (Xử lý lưu)
        [HttpPost]
        public IActionResult Register(int studentId, string courseId)
        {
            try
            {
                _registrationService.Register(studentId, courseId);
                // Thành công thì chuyển về trang Danh sách đăng ký của sinh viên đó
                return RedirectToAction("Index", new { studentId = studentId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // [7] Cancel course
        [HttpPost]
        public IActionResult Cancel(int registrationId, int studentId)
        {
            try
            {
                _registrationService.CancelRegister(registrationId);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi hủy môn: " + ex.Message;
            }
            // Hủy xong quay lại trang danh sách đăng ký của sinh viên đó
            return RedirectToAction("Index", new { studentId = studentId });
        }
    }
}